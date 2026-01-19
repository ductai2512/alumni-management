using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.Shared.DTOs.Donation
{
    public class CreateDonationRequest
    {
        [Required]
        [Range(1000, double.MaxValue)]
        public decimal Amount { get; set; }

        public string Note { get; set; }
    }
}