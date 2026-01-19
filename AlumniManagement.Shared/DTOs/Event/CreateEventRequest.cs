using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.Shared.DTOs.Event
{
    public class CreateEventRequest
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
    }
}
