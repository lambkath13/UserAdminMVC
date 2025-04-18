using Event_Management_System.Models;

namespace Event_Management_System.Repository;

public interface INotificationRepository
{
    Task<List<Notification>> GetAllAsync();
    Task AddAsync(Notification notification);
}