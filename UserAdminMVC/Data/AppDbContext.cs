using UserAdminMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserAdminMVC.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    // note: standard EF Core constructor receiving DbContextOptions
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // important: unique index for NormalizedEmail guarantees no duplicate users
        // nota bene: required by task to handle uniqueness on database level only
        b.Entity<ApplicationUser>()
            .HasIndex(u => u.NormalizedEmail)
            .IsUnique();

        // note: non-unique index for sorting/filtering by LastLoginAt (performance)
        b.Entity<ApplicationUser>()
            .HasIndex(u => u.LastLoginAt);
    }
}