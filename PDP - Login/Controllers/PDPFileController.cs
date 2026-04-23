using Microsoft.AspNetCore.Mvc;
using PDP___Login.Data;
using PDP___Login.Models;

namespace PDP___Login.Controllers
{
    public class PDPFileController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PDPFileController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public IActionResult Upload(int pdpId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected");

            // Create folder path
            string folderPath = Path.Combine(_env.WebRootPath, "PDPFiles");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Unique file name to avoid overwriting
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string fullPath = Path.Combine(folderPath, fileName);

            // Save file to disk
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Save record to DB
            var pdpFile = new PDPFile
            {
                Id = pdpId,
                FileName = file.FileName,
                FilePath = "/PDPFiles/" + fileName
            };

            _context.PDPFiles.Add(pdpFile);
            _context.SaveChanges();

            return RedirectToAction("Index", "PDP");
        }
    }
}
