using Microsoft.EntityFrameworkCore;
using PDP___Login.Data;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// DB CONNECTION
// ==========================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ==========================
// MVC ONLY
// ==========================
builder.Services.AddControllersWithViews();

// Session (important for login)
builder.Services.AddSession();
builder.Services.AddSession();

var app = builder.Build();

// ==========================
// PIPELINE
// ==========================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ✅ REQUIRED for your login system
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();







































//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using PDP___Login.Data;
//using PDP___Login.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();



//builder.Services.AddControllersWithViews()
//    ;
//builder.Services.AddRazorPages();
//var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//    // Ensure roles exist
//    string[] roles = { "HR", "Manager", "Employee" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

//    // Create first admin (HR)
//    var adminEmail = "admin@hr.com";
//    var adminUser = await userManager.FindByEmailAsync(adminEmail);

//    if (adminUser == null)
//    {
//        var user = new ApplicationUser
//        {
//            UserName = adminEmail,
//            Email = adminEmail,
//            FullName = "System Admin",
//            Department = "HR"
//        };

//        var result = await userManager.CreateAsync(user, "Admin@123");

//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(user, "HR");
//        }
//    }
//}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

//app.Run();
