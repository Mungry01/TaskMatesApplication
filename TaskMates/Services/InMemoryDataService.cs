using TaskMates.Models;

namespace TaskMates.Services
{
    public class InMemoryDataService : IDataService
    {
        private readonly List<Idea> _ideas = new();
        private readonly List<Event> _events = new();
        private readonly List<Comment> _comments = new();
        private readonly List<UserProfile> _userProfiles = new();

        public InMemoryDataService()
        {
            SeedData();
        }

        private void SeedData()
        {
            // Seed Ideas
            var idea1 = new Idea
            {
                Id = "1",
                Title = "Campus Sustainability Initiative",
                Description = "Let's start a program to reduce plastic waste on campus by introducing reusable containers in the cafeteria.",
                Course = "Environmental Science",
                Author = "Alex Johnson",
                CreatedDate = DateTime.Now.AddDays(-5),
                Likes = 45,
                Volunteers = 12,
                EndorsedBy = "Dr. Sarah Williams",
                LikedBy = Enumerable.Range(1, 45).Select(i => $"User{i}").ToList(),
                VolunteeredBy = Enumerable.Range(1, 12).Select(i => $"User{i}").ToList()
            };

            var idea2 = new Idea
            {
                Id = "2",
                Title = "Peer Tutoring Platform",
                Description = "Create an online platform where students can offer and request tutoring services for various subjects.",
                Course = "Computer Science",
                Author = "Jamie Lee",
                CreatedDate = DateTime.Now.AddDays(-3),
                Likes = 38,
                Volunteers = 8,
                LikedBy = Enumerable.Range(1, 38).Select(i => $"User{i}").ToList(),
                VolunteeredBy = Enumerable.Range(1, 8).Select(i => $"User{i}").ToList()
            };

            var idea3 = new Idea
            {
                Id = "3",
                Title = "Mental Health Awareness Week",
                Description = "Organize a week of activities focused on mental health awareness, including workshops, guest speakers, and support groups.",
                Course = "Psychology",
                Author = "Morgan Davis",
                CreatedDate = DateTime.Now.AddDays(-7),
                Likes = 67,
                Volunteers = 15,
                EndorsedBy = "Dr. Michael Chen",
                LikedBy = Enumerable.Range(1, 67).Select(i => $"User{i}").ToList(),
                VolunteeredBy = Enumerable.Range(1, 15).Select(i => $"User{i}").ToList()
            };

            _ideas.AddRange(new[] { idea1, idea2, idea3 });

            // Seed Events
            var event1 = new Event
            {
                Id = "1",
                Title = "Annual Science Fair",
                Description = "Showcase student research projects and innovations across all science departments.",
                Author = "Dr. Sarah Williams",
                CreatedDate = DateTime.Now.AddDays(-10),
                ProposedDate = DateTime.Now.AddDays(30),
                Likes = 89,
                Status = EventStatus.Approved,
                IsFacultyChosen = true,
                EndorsedBy = "Dr. Sarah Williams",
                LikedBy = Enumerable.Range(1, 89).Select(i => $"User{i}").ToList()
            };

            var event2 = new Event
            {
                Id = "2",
                Title = "Community Service Day",
                Description = "A day dedicated to volunteering in the local community, including beach cleanup and food bank assistance.",
                Author = "Taylor Smith",
                CreatedDate = DateTime.Now.AddDays(-4),
                ProposedDate = DateTime.Now.AddDays(15),
                Likes = 52,
                Status = EventStatus.UnderReview,
                LikedBy = Enumerable.Range(1, 52).Select(i => $"User{i}").ToList()
            };

            var event3 = new Event
            {
                Id = "3",
                Title = "Cultural Festival",
                Description = "Celebrate diversity through food, music, dance, and art from various cultures.",
                Author = "Casey Brown",
                CreatedDate = DateTime.Now.AddDays(-2),
                ProposedDate = DateTime.Now.AddDays(45),
                Likes = 103,
                Status = EventStatus.Planned,
                EndorsedBy = "Dr. Michael Chen",
                LikedBy = Enumerable.Range(1, 103).Select(i => $"User{i}").ToList()
            };

            _events.AddRange(new[] { event1, event2, event3 });

            // Seed Comments
            _comments.Add(new Comment
            {
                Id = "1",
                Content = "This is a fantastic idea! I'd love to help implement this.",
                AuthorName = "Dr. Sarah Williams",
                AuthorRole = "faculty",
                ItemId = "1",
                ItemType = "idea",
                CreatedDate = DateTime.Now.AddDays(-4)
            });

            _comments.Add(new Comment
            {
                Id = "2",
                Content = "Count me in! When do we start?",
                AuthorName = "Sam Wilson",
                AuthorRole = "student",
                ItemId = "1",
                ItemType = "idea",
                CreatedDate = DateTime.Now.AddDays(-3)
            });
        }

        // Ideas
        public Task<List<Idea>> GetAllIdeasAsync() => Task.FromResult(_ideas.OrderByDescending(i => i.CreatedDate).ToList());

        public Task<Idea?> GetIdeaByIdAsync(string id) => Task.FromResult(_ideas.FirstOrDefault(i => i.Id == id));

        public Task<Idea> CreateIdeaAsync(Idea idea)
        {
            _ideas.Add(idea);
            return Task.FromResult(idea);
        }

        public Task<bool> DeleteIdeaAsync(string id)
        {
            var idea = _ideas.FirstOrDefault(i => i.Id == id);
            if (idea != null)
            {
                _ideas.Remove(idea);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> LikeIdeaAsync(string ideaId, string userName)
        {
            var idea = _ideas.FirstOrDefault(i => i.Id == ideaId);
            if (idea != null)
            {
                if (!idea.LikedBy.Contains(userName))
                {
                    idea.LikedBy.Add(userName);
                    idea.Likes++;
                    return Task.FromResult(true);
                }
                else
                {
                    idea.LikedBy.Remove(userName);
                    idea.Likes--;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> VolunteerForIdeaAsync(string ideaId, string userName)
        {
            var idea = _ideas.FirstOrDefault(i => i.Id == ideaId);
            if (idea != null)
            {
                if (!idea.VolunteeredBy.Contains(userName))
                {
                    idea.VolunteeredBy.Add(userName);
                    idea.Volunteers++;
                    return Task.FromResult(true);
                }
                else
                {
                    idea.VolunteeredBy.Remove(userName);
                    idea.Volunteers--;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> EndorseIdeaAsync(string ideaId, string facultyName)
        {
            var idea = _ideas.FirstOrDefault(i => i.Id == ideaId);
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
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        // Events
        public Task<List<Event>> GetAllEventsAsync() => Task.FromResult(_events.OrderByDescending(e => e.CreatedDate).ToList());

        public Task<Event?> GetEventByIdAsync(string id) => Task.FromResult(_events.FirstOrDefault(e => e.Id == id));

        public Task<Event> CreateEventAsync(Event eventItem)
        {
            _events.Add(eventItem);
            return Task.FromResult(eventItem);
        }

        public Task<bool> DeleteEventAsync(string id)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                _events.Remove(eventItem);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> LikeEventAsync(string eventId, string userName)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == eventId);
            if (eventItem != null)
            {
                if (!eventItem.LikedBy.Contains(userName))
                {
                    eventItem.LikedBy.Add(userName);
                    eventItem.Likes++;
                    return Task.FromResult(true);
                }
                else
                {
                    eventItem.LikedBy.Remove(userName);
                    eventItem.Likes--;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> UpdateEventStatusAsync(string eventId, EventStatus status)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == eventId);
            if (eventItem != null)
            {
                eventItem.Status = status;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> SetFacultyChosenAsync(string eventId, bool isFacultyChosen)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == eventId);
            if (eventItem != null)
            {
                eventItem.IsFacultyChosen = isFacultyChosen;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        // Comments
        public Task<List<Comment>> GetCommentsForItemAsync(string itemId) => 
            Task.FromResult(_comments.Where(c => c.ItemId == itemId).OrderBy(c => c.AuthorRole == "faculty" ? 0 : 1).ThenByDescending(c => c.CreatedDate).ToList());

        public Task<Comment> CreateCommentAsync(Comment comment)
        {
            _comments.Add(comment);
            return Task.FromResult(comment);
        }

        // User Profiles
        public Task<UserProfile?> GetUserProfileAsync(string userName) =>
            Task.FromResult(_userProfiles.FirstOrDefault(u => u.Name == userName));

        public Task<UserProfile> CreateOrUpdateUserProfileAsync(UserProfile profile)
        {
            var existing = _userProfiles.FirstOrDefault(u => u.Name == profile.Name);
            if (existing != null)
            {
                _userProfiles.Remove(existing);
            }
            _userProfiles.Add(profile);
            return Task.FromResult(profile);
        }
    }
}

