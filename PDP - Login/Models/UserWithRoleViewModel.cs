using System.Collections.Generic;
namespace PDP___Login.Models;
public class UserWithRoleViewModel
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }


    public int RoleID { get; set; }
    public List<Role> Roles { get; set; }
    public Role Role { get; set; }
}