using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;
using System.Security.Claims;

namespace PDP___Login.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyPDPs()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            var employee = _context.Employees
                .FirstOrDefault(e => e.UserID == userId.Value);
            var pdps = _context.PDPs
            .Include(p => p.Files)
            .Where(p => p.EmployeeID == employee.EmployeeID)
            .ToList();

            return View(pdps);

        }
        [HttpGet]
        public IActionResult EditSubmission(int id)
        {
            var pdp = _context.PDPs
                .Include(p => p.Files)
                .FirstOrDefault(p => p.Id == id);

            if (pdp == null)
                return NotFound();

            return View(pdp);
        }
        [HttpPost]
        public IActionResult EditSubmission(PDP model, IFormFile file)
        {
            var pdp = _context.PDPs
                .Include(p => p.Files)
                .FirstOrDefault(p => p.Id == model.Id);

            if (pdp == null)
            {
                return NotFound();
            }

            // 1. Update title (if edited)
            pdp.Title = model.Title;

            // 2. Replace file if new one uploaded
            if (file != null && file.Length > 0)
            {
                // Remove old file from folder + DB
                var oldFile = pdp.Files.FirstOrDefault();

                if (oldFile != null)
                {
                    var oldPath = Path.Combine("wwwroot", oldFile.FilePath.TrimStart('/'));

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    _context.PDPFiles.Remove(oldFile);
                }

                // Save new file
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine("wwwroot/pdpfiles", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Add new record
                var newFile = new PDPFile
                {
                    Id = pdp.Id, // FK to PDP
                    FileName = file.FileName,
                    FilePath = "/pdpfiles/" + fileName,
                    SubmittedAt = DateTime.Now
                };

                _context.PDPFiles.Add(newFile);
            }

            _context.SaveChanges();

            return RedirectToAction("MyPDPs");
        }
        public IActionResult EditSubmission()
        {
            return View();


        }
    }
}

