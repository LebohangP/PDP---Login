using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;
using System.Collections.Generic;

namespace PDP___Login.Controllers
{


    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // Show users without roles
        public IActionResult NewRegistrants()
        {
            var roles = _context.Roles.ToList();

            var employees = (from e in _context.Employees
                             join u in _context.Users on e.UserID equals u.UserID
                             select new UserWithRoleViewModel
                             {
                                 UserId = u.UserID,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Department = e.Department,
                                 Roles = roles
                             }).ToList();

            return View(employees);
        }

        // Assign role
        [HttpPost]
        public IActionResult AssignRole(int userId, int roleId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserID == userId);

            if (user == null)
                return NotFound();

            user.RoleID  = roleId;

            _context.Users.Update(user); // 🔥 force tracking update

            _context.SaveChanges();

            return RedirectToAction("Employees");
        }
        public IActionResult Employees()
        {
            var employees = (from e in _context.Employees
                             join u in _context.Users on e.UserID equals u.UserID
                             join r in _context.Roles on u.RoleID equals r.RoleID
                             where u.RoleID != null && r.Name != "No Role"
                             select new UserWithRoleViewModel
                             {
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Department = e.Department,
                                 Email = u.Email,
                                 Role = r
                             }).ToList();

            return View(employees);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }





















































    //[Authorize(Roles = "HR")]
    //public class HRController : Controller
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;

    //    public HRController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    //    {
    //        _signInManager = signInManager;
    //        _userManager = userManager;
    //    }

    //    //[Authorize(Roles = "HR")]
    //    //public async Task<IActionResult> Users()
    //    //{
    //    //    var users = _userManager.Users.ToList();
    //    //    return View(users);
    //    //}
    //    public IActionResult Dashboard()
    //    {
    //        return View();
    //    }
    //    [HttpPost]
    //    public async Task<IActionResult> AssignRole(string userId, string role)
    //    {
    //        var user = await _userManager.FindByIdAsync(userId);

    //        if (user != null)
    //        {
    //            var currentRoles = await _userManager.GetRolesAsync(user);

    //            if (currentRoles.Any())
    //            {
    //                await _userManager.RemoveFromRolesAsync(user, currentRoles);
    //            }

    //            await _userManager.AddToRoleAsync(user, role);
    //        }

    //        return RedirectToAction("Users");
    //    }
    //    public async Task<IActionResult> Index()
    //    {
    //        var users = _userManager.Users.ToList();

    //        var model = new List<UserWithRoleViewModel>();

    //        foreach (var user in users)
    //        {
    //            var roles = await _userManager.GetRolesAsync(user);

    //            model.Add(new UserWithRoleViewModel
    //            {
    //                Id = user.Id,
    //                FullName = user.FullName,
    //                Department = user.Department,
    //                Email = user.Email,
    //                Role = roles.FirstOrDefault() ?? "No Role"
    //            });
    //        }

    //        return View(model); // ✅ correct type
    //    }
    //    public async Task<IActionResult> Users()
    //    {
    //        var users = _userManager.Users.ToList();
    //        var userList = new List<UserWithRoleViewModel>();

    //        foreach (var user in users)
    //        {
    //            var roles = await _userManager.GetRolesAsync(user);

    //            userList.Add(new UserWithRoleViewModel
    //            {
    //                Id = user.Id,
    //                FullName = user.FullName,
    //                Department = user.Department,
    //                Email = user.Email,
    //                Role = roles.FirstOrDefault() ?? "No Role"
    //            });
    //        }

    //        return View(userList);
    //    }

    //}
}
