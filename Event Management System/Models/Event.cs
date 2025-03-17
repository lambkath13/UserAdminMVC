using System.ComponentModel.DataAnnotations;
using Event_Management_System.Enums;

namespace Event_Management_System.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [MaxLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        public EventStatus Status { get; set; } = EventStatus.Active;

        [Required]
        public DateTime EventDate { get; set; }
        
        [MaxLength(100)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(20)]
        public string OrganizerId { get; set; } = null!;
        public User Organizer { get; set; } = null!;

        public List<EventFeedback> EventFeedbacks { get; set; } = null!;
    }

    
}
