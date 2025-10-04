using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserAdminMVC.Data;
using UserAdminMVC.Models;
using UserAdminMVC.Service;

var builder = WebApplication.CreateBuilder(args);

// important: configure database connection (SQL Server)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// note: disable frequent security stamp validation to prevent immediate logout after block
builder.Services.Configure<SecurityStampValidatorOptions>(o =>
{
    o.ValidationInterval = TimeSpan.FromDays(365); // nota bene: user stays authenticated until next action
});

// note: register dependency injection for custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

// important: configure ASP.NET Core Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(opts =>
    {
        // note: task requirement — allow login even if e-mail not confirmed
        opts.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// note: add MVC controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// important: standard middleware pipeline for MVC + Identity
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// nota bene: default route set to Users/Index page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

// note: application entry point
app.Run();