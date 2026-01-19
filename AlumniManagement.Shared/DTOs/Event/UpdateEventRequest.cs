using System;

namespace AlumniManagement.Shared.DTOs.Event
{
    public class UpdateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? EventDate { get; set; }
    }
}