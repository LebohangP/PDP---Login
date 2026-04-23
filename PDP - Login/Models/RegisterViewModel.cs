using System.ComponentModel.DataAnnotations;

namespace PDP___Login.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

    }
}
