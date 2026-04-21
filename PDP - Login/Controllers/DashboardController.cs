using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;
using PDP___Login.Models;

namespace PDP___Login.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var total = _context.PDPs.Count();
            var approved = _context.PDPs.Count(p => p.Status == "Approved");

            var percentApproved = total == 0 ? 0 : (approved * 100) / total;

            var model = new DashboardViewModel
            {
                Total = total,
                Approved = approved,
                Pending = _context.PDPs.Count(p => p.Status == "Pending"),
                Rejected = _context.PDPs.Count(p => p.Status == "Rejected"),
                PercentApproved = percentApproved
            };

            return View(model);
        }
    }
}
