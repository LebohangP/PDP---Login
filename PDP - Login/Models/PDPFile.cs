namespace PDP___Login.Models
{
    public class PDPFile
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public int PDPId { get; set; }
        public virtual PDP PDP { get; set; }
    }
}
