namespace PDP___Login.Models
{
    public class PDPFile
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmittedAt {  get; set; }

        public int Id { get; set; }
        public PDP PDP { get; set; }
    }
}
