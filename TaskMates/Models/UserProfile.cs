using System.ComponentModel.DataAnnotations;

namespace TaskMates.Models
{
    public class UserProfile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // In production, this should be hashed
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // student, faculty, admin
        public string Position { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; } = DateTime.Now;
        
        // Student-specific
        public List<string> SubmittedIdeaIds { get; set; } = new List<string>();
        public List<string> SubmittedEventIds { get; set; } = new List<string>();
        public List<string> VolunteerHistory { get; set; } = new List<string>();
        
        // Faculty-specific
        public List<string> EndorsedIdeaIds { get; set; } = new List<string>();
        public List<string> EndorsedEventIds { get; set; } = new List<string>();
        
        // Admin-specific
        public int TotalEventsManaged { get; set; } = 0;
        public int TotalIdeasManaged { get; set; } = 0;
    }
}

