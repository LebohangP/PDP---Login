using Microsoft.EntityFrameworkCore;
using PDP___Login.Models;
using System.IO.Compression;

namespace PDP___Login.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PDP> PDPs { get; set; }
        public DbSet<PDPFile> PDPFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map to exact table names if needed
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<PDP>().ToTable("PDP");
            modelBuilder.Entity<PDPFile>().ToTable("PDPFiles");

            modelBuilder.Entity<PDPFile>()
                .HasKey(f => f.FileID); // PK

            modelBuilder.Entity<PDPFile>()
                .HasOne(f => f.PDP)
                .WithMany(p => p.Files) 
                .HasForeignKey(f => f.Id); // FK
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Role>()
            //   .HasOne(f => f.PDP)
            //   .WithMany(p => p.Files)
            //   .HasForeignKey(f => f.Id); // FK
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleID);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}
