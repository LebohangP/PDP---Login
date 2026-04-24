using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;

namespace PDP___Login.Controllers
{

    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;

        public ManagerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        // ALL SUBMITTED PDPs
        public IActionResult Index()
        {
            var pdps = _context.PDPs
                .Where(p => p.Status == "Pending") // 🔥 only pending
                .Include(p => p.Files)
                .Include(p => p.Employee)
                .ToList();

            return View(pdps);
        }
        public IActionResult UpdateStatus(int id, string status,PDP model)
        {
            var pdp = _context.PDPs.FirstOrDefault(p => p.Id == id);
            var comment = model.Comment;

            if (pdp == null)
            {
                return NotFound();
            }

            pdp.Status = status;

            _context.SaveChanges();

            if (status == "Approved")
            {
                return RedirectToAction("ApprovedPDPs");
            }
            else if (status == "Rejected")
            {
                pdp.Comment = comment;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ApprovedPDPs()
        {
            var pdps = _context.PDPs
                .Where(p => p.Status == "Approved")
                .Include(p => p.Files)
                .Include(p => p.Employee)
                .ToList();

            return View(pdps);
        }
        public IActionResult RejectedPDPs()
        {
            var pdps = _context.PDPs
                .Where(p => p.Status == "Rejected")
                .Include(p => p.Files)
                .Include(p => p.Employee)
                .ToList();

            return View(pdps);
        }
    }
}

