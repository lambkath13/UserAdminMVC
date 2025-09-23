namespace JobBOT.Models;

public class Vacancy
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public Guid BranchId { get; set; }
    public Branch? Branch { get; set; }
}
