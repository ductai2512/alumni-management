using System;

namespace AlumniManagement.Shared.DTOs.Event
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
