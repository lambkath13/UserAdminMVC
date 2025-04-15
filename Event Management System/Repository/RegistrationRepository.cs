using Event_Management_System.Data;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class RegistrationRepository:IRegistrationRepository
{
    private readonly AppDbContext _context;
    
    public async Task AddAsync(EventRegistration registration)
    {
        var exist = await _context.EventRegistrations
            .AnyAsync(r => r.EventId == registration.EventId && r.UserId == registration.UserId);
        
        if (exist)  
            return; //если он Submit то ничего не делаем
        
        await _context.EventRegistrations.AddAsync(registration);
        await _context.SaveChangesAsync();    
    }

    public async Task RemoveAsync(int eventId, Guid userId)
    {
        var registration = await _context.EventRegistrations
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);
        
        if (registration != null)
        {
            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}