using System;

namespace AlumniManagement.Shared.DTOs.Donation
{
    public class DonationDto
    {
        public int DonationId { get; set; }
        public int AlumniId { get; set; }
        public string AlumniName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public string Note { get; set; }
    }
}
