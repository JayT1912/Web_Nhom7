using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Nhom7_webTourdulich.Services;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Register IUserRepository with EFUserRepository
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Đặt thời gian timeout cho session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSession(); // Thêm session
builder.Services.AddAuthentication(); // Thêm xác thực

// Add DbContext and configure connection string for SQL Server
builder.Services.AddDbContext<QuanLyTourContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add authentication with cookies (for login, logout, and access denied)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied"; // Path when access is denied
    });

// Add session support with a timeout of 30 minutes
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add authorization policy for roles (like Admin, Manager)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrManagePolicy", policy =>
        policy.RequireRole("Admin", "Manager")); // Admin and Manager roles
});

// Register MVC services
builder.Services.AddControllersWithViews();

// Register email sender service for email functionalities
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure middleware pipeline for the application
if (!app.Environment.IsDevelopment())
{
    // Use exception handler and HSTS in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // HTTP Strict Transport Security for production
}

app.UseHttpsRedirection();  // Redirect HTTP to HTTPS
app.UseStaticFiles();  // Serve static files (CSS, JS, images, etc.)

// Middleware for routing and authorization
app.UseRouting();
app.UseAuthentication();  // Ensure authentication is called before authorization
app.UseAuthorization();   // Use authorization middleware
app.UseSession();         // Enable session management

// Configure default route for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();  // Run the application
