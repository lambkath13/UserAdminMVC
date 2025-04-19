namespace Event_Management_System.Dto.User;

public class GetUserDto
{
    public List<UserDto> Entities { get; set; }
    public Guid? UserId { get; set; }
    
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }


    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}