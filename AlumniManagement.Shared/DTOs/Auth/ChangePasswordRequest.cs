using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.Shared.DTOs.Auth
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
