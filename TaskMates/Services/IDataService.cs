using TaskMates.Models;

namespace TaskMates.Services
{
    public interface IDataService
    {
        // Ideas
        Task<List<Idea>> GetAllIdeasAsync();
        Task<Idea?> GetIdeaByIdAsync(string id);
        Task<Idea> CreateIdeaAsync(Idea idea);
        Task<bool> DeleteIdeaAsync(string id);
        Task<bool> LikeIdeaAsync(string ideaId, string userName);
        Task<bool> VolunteerForIdeaAsync(string ideaId, string userName);
        Task<bool> EndorseIdeaAsync(string ideaId, string facultyName);

        // Events
        Task<List<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(string id);
        Task<Event> CreateEventAsync(Event eventItem);
        Task<bool> DeleteEventAsync(string id);
        Task<bool> LikeEventAsync(string eventId, string userName);
        Task<bool> UpdateEventStatusAsync(string eventId, EventStatus status);
        Task<bool> SetFacultyChosenAsync(string eventId, bool isFacultyChosen);

        // Comments
        Task<List<Comment>> GetCommentsForItemAsync(string itemId);
        Task<Comment> CreateCommentAsync(Comment comment);

        // User Profiles
        Task<UserProfile?> GetUserProfileAsync(string userName);
        Task<UserProfile> CreateOrUpdateUserProfileAsync(UserProfile profile);
    }
}

