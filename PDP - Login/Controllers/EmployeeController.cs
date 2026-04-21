using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;
using System.Security.Claims;

namespace PDP___Login.Controllers
{
    [Authorize(Roles ="Employee")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EmployeeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public class PDPController : Controller
        {
            private readonly ApplicationDbContext _context;

            public PDPController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET
            public IActionResult AddPDPs()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                var model = new UserPDPViewModel
                {
                    FullName = user.FullName,
                    Department = user.Department
                };

                return View(model);
            }

            // POST
            [HttpPost]
            public async Task<IActionResult> AddPDPs(UserPDPViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                string filePath = null;

                // Upload PDF
                if (model.PdfFile != null && model.PdfFile.Length > 0)
                {
                    var fileName = Path.GetFileName(model.PdfFile.FileName);
                    var path = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.PdfFile.CopyToAsync(stream);
                    }

                    filePath = "/uploads/" + fileName;
                }

                var pdp = new PDP
                {
                    Description = model.Description,
                    DateSubmitted = DateTime.Now,
                    UserId = userId,
                    FilePath = filePath,
                    Status = "Pending"


                };

                _context.PDPs.Add(pdp);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyPDPs");
            }
            public IActionResult MyPDPs()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var pdps = _context.PDPs
                    .Where(p => p.UserId == userId)
                    .ToList();

                return View(pdps);
            }
            public IActionResult Index()
            {
                return View();
            }


        }
    }
}
