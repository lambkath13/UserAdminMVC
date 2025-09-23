namespace JobBOT.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
}
