using System.ComponentModel.DataAnnotations;

namespace TaskMates.Models
{
    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ProposedDate { get; set; }
        public int ExpectedParticipants { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public EventStatus Status { get; set; } = EventStatus.UnderReview;
        public string? EndorsedBy { get; set; }
        public bool IsEndorsed => !string.IsNullOrEmpty(EndorsedBy);
        public List<string> CommentIds { get; set; } = new List<string>();
        public List<string> LikedBy { get; set; } = new List<string>();
        public bool IsFacultyChosen { get; set; } = false;
    }

    public enum EventStatus
    {
        UnderReview,
        Approved,
        Planned,
        Rejected
    }
}

