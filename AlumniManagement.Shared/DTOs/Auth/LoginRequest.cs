using System.ComponentModel.DataAnnotations;

namespace AlumniManagement.Shared.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}