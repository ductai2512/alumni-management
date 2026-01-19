namespace AlumniManagement.Shared.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int? AlumniId { get; set; }
        public string FullName { get; set; }
    }
}