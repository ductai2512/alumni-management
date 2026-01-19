using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.DAL.Entities
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        public int AlumniId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        // Navigation properties
        public Alumni Alumni { get; set; }
    }
}