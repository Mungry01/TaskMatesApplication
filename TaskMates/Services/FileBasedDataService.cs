using TaskMates.Models;

namespace TaskMates.Services
{
    public class FileBasedDataService : IDataService
    {
        private readonly FileStorageService _fileStorage;

        public FileBasedDataService(FileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        // Ideas
        public async Task<List<Idea>> GetAllIdeasAsync()
        {
            var ideas = await _fileStorage.GetAllIdeasAsync();
            return ideas.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public async Task<Idea?> GetIdeaByIdAsync(string id)
        {
            return await _fileStorage.GetIdeaByIdAsync(id);
        }

        public async Task<Idea> CreateIdeaAsync(Idea idea)
        {
            return await _fileStorage.CreateIdeaAsync(idea);
        }

        public async Task<bool> DeleteIdeaAsync(string id)
        {
            return await _fileStorage.DeleteIdeaAsync(id);
        }

        public async Task<bool> LikeIdeaAsync(string ideaId, string userName)
        {
            var idea = await _fileStorage.GetIdeaByIdAsync(ideaId);
            if (idea != null)
            {
                if (!idea.LikedBy.Contains(userName))
                {
                    idea.LikedBy.Add(userName);
                    idea.Likes++;
                }
                else
                {
                    idea.LikedBy.Remove(userName);
                    idea.Likes--;
                }
                await _fileStorage.UpdateIdeaAsync(idea);
                return true;
            }
            return false;
        }

        public async Task<bool> VolunteerForIdeaAsync(string ideaId, string userName)
        {
            var idea = await _fileStorage.GetIdeaByIdAsync(ideaId);
            if (idea != null)
            {
                if (!idea.VolunteeredBy.Contains(userName))
                {
                    idea.VolunteeredBy.Add(userName);
                    idea.Volunteers++;
                }
                else
                {
                    idea.VolunteeredBy.Remove(userName);
                    idea.Volunteers--;
                }
                await _fileStorage.UpdateIdeaAsync(idea);
                return true;
            }
            return false;
        }

        public async Task<bool> EndorseIdeaAsync(string ideaId, string facultyName)
        {
            var idea = await _fileStorage.GetIdeaByIdAsync(ideaId);
            if (idea != null)
            {
                // Toggle endorsement: if already endorsed by this faculty, cancel it
                if (idea.EndorsedBy == facultyName)
                {
                    idea.EndorsedBy = null; // Cancel endorsement
                }
                else
                {
                    idea.EndorsedBy = facultyName; // Endorse
                }
                await _fileStorage.UpdateIdeaAsync(idea);
                return true;
            }
            return false;
        }

        // Events
        public async Task<List<Event>> GetAllEventsAsync()
        {
            var events = await _fileStorage.GetAllEventsAsync();
            return events.OrderByDescending(e => e.CreatedDate).ToList();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await _fileStorage.GetEventByIdAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event eventItem)
        {
            return await _fileStorage.CreateEventAsync(eventItem);
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            return await _fileStorage.DeleteEventAsync(id);
        }

        public async Task<bool> LikeEventAsync(string eventId, string userName)
        {
            var eventItem = await _fileStorage.GetEventByIdAsync(eventId);
            if (eventItem != null)
            {
                if (!eventItem.LikedBy.Contains(userName))
                {
                    eventItem.LikedBy.Add(userName);
                    eventItem.Likes++;
                }
                else
                {
                    eventItem.LikedBy.Remove(userName);
                    eventItem.Likes--;
                }
                await _fileStorage.UpdateEventAsync(eventItem);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateEventStatusAsync(string eventId, EventStatus status)
        {
            var eventItem = await _fileStorage.GetEventByIdAsync(eventId);
            if (eventItem != null)
            {
                eventItem.Status = status;
                await _fileStorage.UpdateEventAsync(eventItem);
                return true;
            }
            return false;
        }

        public async Task<bool> SetFacultyChosenAsync(string eventId, bool isFacultyChosen)
        {
            var eventItem = await _fileStorage.GetEventByIdAsync(eventId);
            if (eventItem != null)
            {
                eventItem.IsFacultyChosen = isFacultyChosen;
                await _fileStorage.UpdateEventAsync(eventItem);
                return true;
            }
            return false;
        }

        // Comments - Store in memory for now (can be extended to file storage later)
        private readonly List<Comment> _comments = new();

        public Task<List<Comment>> GetCommentsForItemAsync(string itemId)
        {
            return Task.FromResult(_comments
                .Where(c => c.ItemId == itemId)
                .OrderBy(c => c.AuthorRole == "faculty" ? 0 : 1)
                .ThenByDescending(c => c.CreatedDate)
                .ToList());
        }

        public Task<Comment> CreateCommentAsync(Comment comment)
        {
            _comments.Add(comment);
            return Task.FromResult(comment);
        }

        // User Profiles - Use FileStorageService
        public async Task<UserProfile?> GetUserProfileAsync(string userName)
        {
            var users = await _fileStorage.GetAllUsersAsync();
            return users.FirstOrDefault(u => u.Name == userName || u.Username == userName);
        }

        public async Task<UserProfile> CreateOrUpdateUserProfileAsync(UserProfile profile)
        {
            var existing = await _fileStorage.GetUserByIdAsync(profile.Id);
            if (existing != null)
            {
                await _fileStorage.UpdateUserAsync(profile);
            }
            else
            {
                await _fileStorage.CreateUserAsync(profile);
            }
            return profile;
        }
    }
}

