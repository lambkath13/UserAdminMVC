using Event_Management_System.Data;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class NotificationRepository(AppDbContext context) : INotificationRepository
{
    public async Task<List<Notification>> GetAllAsync()
    {
        return await context.Notifications
            .OrderByDescending(x => x.CreatedAt)
            .Take(3).ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
        await context.SaveChangesAsync();
    }
}