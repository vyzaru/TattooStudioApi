using System;
using System.Collections.Generic;

namespace PetAPI.Models
{
    public partial class Master
    {
        public Master()
        {
            Appointments = new HashSet<Appointment>();
            Tattoos = new HashSet<Tattoo>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? PortfolioUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Tattoo> Tattoos { get; set; }
    }
}
