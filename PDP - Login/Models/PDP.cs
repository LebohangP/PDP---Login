namespace PDP___Login.Models
{
    public class PDP
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmitted { get; set; }= DateTime.Now;

        public string UserId {  get; set; }
        public virtual ApplicationUser User {  get; set; }
        public string FilePath { get; set; } 

        public string Status {  get; set; }
    }
}
