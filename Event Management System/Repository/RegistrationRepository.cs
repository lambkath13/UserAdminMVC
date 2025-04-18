using Event_Management_System.Data;
using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository;

public class RegistrationRepository(AppDbContext context) : IRegistrationRepository
{
    
    public async Task AddAsync(EventRegistration registration)
    {
        var exist = await context.EventRegistrations
            .AnyAsync(r => r.EventId == registration.EventId && r.UserId == registration.UserId);
        
        if (exist)  
            return; //если он Submit то ничего не делаем
        
        await context.EventRegistrations.AddAsync(registration);
        await context.SaveChangesAsync();    
    }

    public async Task RemoveAsync(EventRegistration registration)
    {
      
            context.EventRegistrations.Remove(registration);
            await context.SaveChangesAsync();
    }

    public bool GetByEventId(int argId)
    {
        return context.EventRegistrations.Any(x => x.EventId == argId);
    }

    public Task<EventRegistration?> GetByUserIdAndEventId(Guid registrationDtoUserId, int registrationDtoEventId)
    {
        return context.EventRegistrations.FirstOrDefaultAsync(x => x.UserId == registrationDtoUserId && x.EventId == registrationDtoEventId); 
    }
}