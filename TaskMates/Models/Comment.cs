using System.ComponentModel.DataAnnotations;

namespace TaskMates.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty; // student, faculty, admin
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ItemId { get; set; } = string.Empty; // References Idea or Event ID
        public string ItemType { get; set; } = string.Empty; // "idea" or "event"
    }
}

