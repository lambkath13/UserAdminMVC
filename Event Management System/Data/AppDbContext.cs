using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Event_Management_System.Data;

    
public class AppDbContext : DbContext
{
    private readonly UserManager<User> _userManager;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }
    public DbSet<User> Users { get; set; } 
    public DbSet<Event> Events { get; set; } 
    public DbSet<EventFeedback> EventFeedbacks { get; set; } 
    public DbSet<Post> Posts { get; set; } 
    public DbSet<PostComment> PostComments { get; set; } 
    public DbSet<Image> Images { get; set; } 
    public DbSet<LoginRequest> LoginRequests { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Указываем, что у Event один Organizer (User)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.OrganizerId)
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

        // Связь между Image и Post
        modelBuilder.Entity<Image>()
            .HasOne(i => i.Post)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}