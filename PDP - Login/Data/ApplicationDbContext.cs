using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PDP___Login.Models;
using System.IO.Compression;

namespace PDP___Login.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PDP> PDPs { get; set; }

        public DbSet<PDPFile> PDPFiles { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
