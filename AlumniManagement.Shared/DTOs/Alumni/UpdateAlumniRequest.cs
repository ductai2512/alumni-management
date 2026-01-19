using System;

namespace AlumniManagement.Shared.DTOs.Alumni
{
    public class UpdateAlumniRequest
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string CurrentJob { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
    }
}
