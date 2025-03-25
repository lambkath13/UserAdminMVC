using System.Globalization;
using Event_Management_System.Enums;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Event_Management_System.Data;

    
public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }
    public DbSet<User> Users { get; set; } 
    public DbSet<Event> Events { get; set; } 
    public DbSet<EventFeedback> EventFeedbacks { get; set; } 
    public DbSet<Post> Posts { get; set; } 
    public DbSet<PostComment> PostComments { get; set; } 
    public DbSet<EventRegistration> EventRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var adminUser = new User
        {
            Id = new Guid("fc4552da-13bc-479c-94a5-b816fd2ca4ca"),
            PassportId = "ADMIN123456",
            Name = "Admin",
            AvatarUrl = null,
            //UserName = "admin",
            Role = UserRole.Admin,
            IsFirstAdmin = true,
            Email = "admin@example.com",
            // NormalizedUserName = "ADMIN",
            // NormalizedEmail = "ADMIN@EXAMPLE.COM",
            // EmailConfirmed = true,
            // AccessFailedCount = 0,
            // ConcurrencyStamp = string.Empty,
            // PhoneNumber = "",
            // LockoutEnabled = false,
            // PhoneNumberConfirmed = false,
            // LockoutEnd = null,
            // SecurityStamp = null,
            // TwoFactorEnabled = false,
            PasswordHash = "$2b$10$QjPm39leFRKCOULaXj4ej.oQ8f4sUb6ITpPWBrZteQgGYb/y83SJu" //BCrypt.Net.BCrypt.HashPassword("Admin123!")
        };
        modelBuilder.Entity<User>().HasData(adminUser);

        // Указываем, что у Event один Organizer (User)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.User)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Запрещаем каскадное удаление

        // Связь между EventFeedback и User
        modelBuilder.Entity<EventFeedback>()
            .HasOne(ef => ef.User)
            .WithMany()
            .HasForeignKey(ef => ef.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь между EventFeedback и Event
        modelBuilder.Entity<EventFeedback>()
            .HasOne(ef => ef.Event)
            .WithMany(e => e.EventFeedbacks)
            .HasForeignKey(ef => ef.EventId)
            .OnDelete(DeleteBehavior.Cascade); // Удаляем отзывы, если удаляется событие
   
        // Связь между Post и User
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь между PostComment и Post
        modelBuilder.Entity<PostComment>()
            .HasOne(pc => pc.Post)
            .WithMany(p => p.PostComments)
            .HasForeignKey(pc => pc.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь между PostComment и User
        modelBuilder.Entity<PostComment>()
            .HasOne(pc => pc.User)
            .WithMany()
            .HasForeignKey(pc => pc.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}