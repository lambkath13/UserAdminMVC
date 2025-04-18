namespace Event_Management_System.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsNew { get; set; }
    public Guid? UserId { get; set; }
}