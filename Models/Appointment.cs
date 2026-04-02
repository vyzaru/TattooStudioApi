using System;
using System.Collections.Generic;

namespace PetAPI.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string ClientPhone { get; set; } = null!;
        public string? ClientEmail { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DurationHours { get; set; }
        public string? Notes { get; set; }
        public int? TattooId { get; set; }
        public int MasterId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Master Master { get; set; } = null!;
        public virtual Tattoo? Tattoo { get; set; }
    }
}
