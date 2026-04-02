using System;
using System.Collections.Generic;

namespace PetAPI.Models
{
    public partial class Tattoo
    {
        public Tattoo()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Style { get; set; } = null!;
        public string BodyPlacement { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int MasterId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Master Master { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
