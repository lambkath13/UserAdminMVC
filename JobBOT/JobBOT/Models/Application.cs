namespace JobBOT.Models;

public class Application
{
    public int Id { get; set; }
    public int VacancyId { get; set; }
    public Vacancy Vacancy { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? BirthDate { get; set; }
    public string? Phone { get; set; }
}