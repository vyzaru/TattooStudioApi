using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PetAPI.Models;

namespace PetAPI.Data
{
    public partial class TattooStudioDbContext : DbContext
    {
        public TattooStudioDbContext()
        {
        }

        public TattooStudioDbContext(DbContextOptions<TattooStudioDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Master> Masters { get; set; } = null!;
        public virtual DbSet<Tattoo> Tattoos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=tattoostudio;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("appointment_status", new[] { "Scheduled", "Completed", "Cancelled" });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("timestamp without time zone");

                entity.Property(e => e.ClientEmail).HasMaxLength(200);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClientPhone).HasMaxLength(20);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DurationHours).HasDefaultValueSql("1");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("Appointments_MasterId_fkey");

                entity.HasOne(d => d.Tattoo)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TattooId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Appointments_TattooId_fkey");
            });

            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasIndex(e => e.Email, "Masters_Email_key")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PortfolioUrl).HasMaxLength(500);

                entity.Property(e => e.Specialization).HasMaxLength(100);
            });

            modelBuilder.Entity<Tattoo>(entity =>
            {
                entity.Property(e => e.BodyPlacement).HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.Price).HasPrecision(10, 2);

                entity.Property(e => e.Style).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.Tattoos)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("Tattoos_MasterId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
