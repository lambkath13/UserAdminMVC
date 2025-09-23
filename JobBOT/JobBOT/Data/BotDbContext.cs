using JobBOT.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBOT.Data;

public class BotDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Vacancy> Vacancies => Set<Vacancy>();
    public DbSet<Application> Applications => Set<Application>();

    public BotDbContext(DbContextOptions<BotDbContext> options)
        : base(options) { }
}