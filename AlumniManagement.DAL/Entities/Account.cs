using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.DAL.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Admin, Alumni

        [Required] 
        public int? AlumniId { get; set; }

        public bool IsLocked { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Alumni Alumni { get; set; }
    }
}
