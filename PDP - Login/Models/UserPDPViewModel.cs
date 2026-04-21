namespace PDP___Login.Models
{
    public class UserPDPViewModel
    {
        public string FullName { get; set; }
        public string Department { get; set; }

        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public IFormFile PdfFile { get; set; }
    }
}
