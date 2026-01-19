using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.DAL.Entities
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        public int CreatedBy { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<AlumniEvent> AlumniEvents { get; set; }
    }
}