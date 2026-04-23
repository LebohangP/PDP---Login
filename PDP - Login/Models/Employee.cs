namespace PDP___Login.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string Department { get; set; }

        public int UserID { get; set; }  
        public User User { get; set; }    
        public ICollection<PDP> PDPs { get; set; }
    }
}
