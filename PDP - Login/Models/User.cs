using System.Data;

namespace PDP___Login.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }

        public Employee Employee { get; set; } // one-to-one
    }
}
