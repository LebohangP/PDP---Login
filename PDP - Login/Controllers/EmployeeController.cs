using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;
using System.Security.Claims;

namespace PDP___Login.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // ============================
        // DASHBOARD
        // ============================
        public IActionResult Index()
        {
            return View();
        }

        // ============================
        // GET: Submit PDP Form
        // ============================
        public async Task<IActionResult> SubmitPDP()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            var model = new UserPDPViewModel
            {
                FullName = user.FullName,
                Department = user.Department
            };

            return View(model);
        }

        // ============================
        // POST: Submit PDP
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitPDP(UserPDPViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            // 🔥 re-fill values
            model.FullName = user.FullName;
            model.Department = user.Department;

            if (!ModelState.IsValid)
                return View(model);

            string filePath = null;

            if (model.PdfFile != null && model.PdfFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PdfFile.FileName);
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.PdfFile.CopyToAsync(stream);
                }

                filePath = "/uploads/" + fileName;
            }

            var pdp = new PDP
            {
                Description = model.Description,
                DateSubmitted = DateTime.Now,
                UserId = user.Id,
                FilePath = filePath,
                Status = "Pending"
            };

            _context.PDPs.Add(pdp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyPDPs));
        }

        // ============================
        // VIEW: My PDP Statuses
        // ============================
        public IActionResult MyPDPs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pdps = _context.PDPs
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.DateSubmitted)
                .ToList();

            return View(pdps);
        }
    }



}

