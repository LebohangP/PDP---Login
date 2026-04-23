using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;
using System.Security.Claims;

namespace PDP___Login.Controllers
{

    public class PDPController : Controller
    {
        private readonly AppDbContext _context;

        public PDPController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PDP list
        public IActionResult Index()
        {
            var pdps = _context.PDPs
                .Include(p => p.Employee)
                .ToList();

            return View(pdps);
        }

        // GET: Create PDP
        public IActionResult SubmitPDP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitPDP(PDP model, IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Employee");
            }

            var employee = _context.Employees
                .FirstOrDefault(e => e.UserID == userId.Value);

            if (employee == null)
            {
                return Content("No Employee record found. Please contact admin.");
            }

            model.EmployeeID = employee.EmployeeID;
            model.SubmittedAt = DateTime.Now;
            model.Status = "Pending";

            _context.PDPs.Add(model);
            _context.SaveChanges();

            // Save file
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine("wwwroot/pdpfiles", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _context.PDPFiles.Add(new PDPFile
                {
                    Id = model.Id,
                    FileName = fileName,
                    FilePath = "/pdpfiles/" + fileName
                });

                _context.SaveChanges();
            }
            var pdps = _context.PDPs
        .Include(p => p.Files)
        .Where(p => p.EmployeeID == employee.EmployeeID)
        .ToList();

            return RedirectToAction("MyPDPs","Employees");
        }
        public IActionResult MyPDPs()
        {
            var pdps = _context.PDPs.ToList();
            return View(pdps);
        }
       



    }







































    //[Authorize(Roles = "Employee")]
    //public class PDPController : Controller
    //{
    //    private readonly ApplicationDbContext _context;

    //    public PDPController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET
    //    public IActionResult AddPDPs()
    //    {
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    //        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

    //        var model = new UserPDPViewModel
    //        {
    //            FullName = user.FullName,
    //            Department = user.Department
    //        };

    //        return View(model);
    //    }

    //    // POST
    //    [HttpPost]
    //    public async Task<IActionResult> AddPDPs(UserPDPViewModel model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View(model);
    //        }

    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    //        string filePath = null;

    //        // Upload PDF
    //        if (model.PdfFile != null && model.PdfFile.Length > 0)
    //        {
    //            var fileName = Path.GetFileName(model.PdfFile.FileName);
    //            var path = Path.Combine("wwwroot/uploads", fileName);

    //            Directory.CreateDirectory("wwwroot/uploads"); // ensure folder exists

    //            using (var stream = new FileStream(path, FileMode.Create))
    //            {
    //                await model.PdfFile.CopyToAsync(stream);
    //            }

    //            filePath = "/uploads/" + fileName;
    //        }

    //        var pdp = new PDP
    //        {
    //            Description = model.Description,
    //            DateSubmitted = DateTime.Now,
    //            UserId = userId,
    //            FilePath = filePath,
    //            Status = "Pending"
    //        };

    //        _context.PDPs.Add(pdp);
    //        await _context.SaveChangesAsync();

    //        return RedirectToAction("MyPDPs");
    //    }

    //    public IActionResult MyPDPs()
    //    {
    //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    //        var pdps = _context.PDPs
    //            .Where(p => p.UserId == userId)
    //            .ToList();

    //        return View(pdps);
    //    }
    //}
}
