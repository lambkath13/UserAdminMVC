namespace Event_Management_System.Dto.Event;

public class GetEventEntityDto
{
    public List<GetAllEventDto> Entities { get; set; }
    public Guid? UserId { get; set; }
}