using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDP___Login.Data;
using PDP___Login.Models;

namespace PDP___Login.Controllers
{

    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var defaultRole = _context.Roles.FirstOrDefault(r => r.Name == "No Role");


            var user = new User
            {
                Email = model.Email,
                PasswordHash = model.Password,
                Role = defaultRole


            };

            _context.Users.Add(user);
            _context.SaveChanges();
            var UserId = user.UserID;

            // 2. Create Employee profile
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Department = model.Department,
                UserID = UserId,
                Email = model.Email,
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            TempData["Success"] = "Account successfully created. Please log in.";

            return RedirectToAction("Login");
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid login";
                return View();
            }

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Email", user.Email);


            // 🚨 BLOCK users without roles
            if (user.RoleID == 4)
            {

                return View("NoRole");
            }

            // Save session
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetInt32("RoleID", user.RoleID);

            // Redirect by role
            if (user.RoleID == 1)
                return RedirectToAction("Dashboard", "Admin");

            if (user.RoleID == 3)
                return RedirectToAction("Dashboard", "Manager");

            if (user.RoleID == 2)
                return RedirectToAction("Index", "Employees");


            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}



