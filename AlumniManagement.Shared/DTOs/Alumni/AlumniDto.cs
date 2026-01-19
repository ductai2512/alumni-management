using System;

namespace AlumniManagement.Shared.DTOs.Alumni
{
    public class AlumniDto
    {
        public int AlumniId { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int GraduationYear { get; set; }
        public string Major { get; set; }
        public string CurrentJob { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
