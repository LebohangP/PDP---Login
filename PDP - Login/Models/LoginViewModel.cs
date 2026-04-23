using System.ComponentModel.DataAnnotations;

namespace PDP___Login.Models
{
    public class LoginViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Password { get; internal set; }
    }
}
//Scaffold-DbContext "Server=LEBOHANGP-ICT-N\\LBHNG;Database=NewDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AppDbContext -f
