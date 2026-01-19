using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.DAL.Entities
{
    public class AlumniEvent
    {
        public int AlumniId { get; set; }
        public int EventId { get; set; }
        public DateTime RegisterDate { get; set; }

        // Navigation properties
        public Alumni Alumni { get; set; }
        public Event Event { get; set; }
    }
}