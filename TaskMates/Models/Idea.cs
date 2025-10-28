using System.ComponentModel.DataAnnotations;

namespace TaskMates.Models
{
    public class Idea
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Likes { get; set; } = 0;
        public int Volunteers { get; set; } = 0;
        public string? EndorsedBy { get; set; }
        public bool IsEndorsed => !string.IsNullOrEmpty(EndorsedBy);
        public List<string> CommentIds { get; set; } = new List<string>();
        public List<string> LikedBy { get; set; } = new List<string>();
        public List<string> VolunteeredBy { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
