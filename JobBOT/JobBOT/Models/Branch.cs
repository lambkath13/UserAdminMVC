namespace JobBOT.Models;

public class Branch
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    
    List<Vacancy>? Vacancies { get; set; } 
}
