using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.Shared.DTOs.Alumni
{
    public class CreateAlumniRequest
    {
        [Required]
        public string StudentCode { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public int GraduationYear { get; set; }

        public string Major { get; set; }
        public string CurrentJob { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
    }
}