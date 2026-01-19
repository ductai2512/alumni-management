namespace AlumniManagement.Shared.DTOs.Alumni
{
    public class AlumniSearchRequest
    {
        public string Keyword { get; set; }
        public int? GraduationYear { get; set; }
        public string Major { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}