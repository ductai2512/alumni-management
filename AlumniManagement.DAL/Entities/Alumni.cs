using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.DAL.Entities
{
    public class Alumni
    {
        [Key]
        public int AlumniId { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public int GraduationYear { get; set; }

        [StringLength(100)]
        public string Major { get; set; }

        [StringLength(100)]
        public string? CurrentJob { get; set; }

        [StringLength(100)]
        public string? Company { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public Account Account { get; set; }
        public ICollection<AlumniEvent> AlumniEvents { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }
}