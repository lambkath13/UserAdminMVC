using Event_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Event_Management_System.DBContextFactory;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Event_Management;User Id=SA;Password=uZXS4nEmGfqIp4yJAyQ==;TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
