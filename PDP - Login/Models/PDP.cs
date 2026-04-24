namespace PDP___Login.Models
{
    public class PDP
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Status { get; set; } = "Pending";
        public DateTime SubmittedAt { get; set; }
        public string? Comment { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public ICollection<PDPFile> Files { get; set; }
    }
}
