using System.Text.Json;
using TaskMates.Models;

namespace TaskMates.Services
{
    public class FileStorageService
    {
        private readonly string _dataDirectory;
        private readonly JsonSerializerOptions _jsonOptions;

        public FileStorageService()
        {
            _dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Storage");
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Ensure data directory exists
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
        }

        // Ideas CRUD operations
        public async Task<List<Idea>> GetAllIdeasAsync()
        {
            var ideasFile = Path.Combine(_dataDirectory, "ideas.json");
            if (!File.Exists(ideasFile))
            {
                return new List<Idea>();
            }

            var json = await File.ReadAllTextAsync(ideasFile);
            return JsonSerializer.Deserialize<List<Idea>>(json, _jsonOptions) ?? new List<Idea>();
        }

        public async Task<Idea?> GetIdeaByIdAsync(string id)
        {
            var ideas = await GetAllIdeasAsync();
            return ideas.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Idea> CreateIdeaAsync(Idea idea)
        {
            var ideas = await GetAllIdeasAsync();
            ideas.Add(idea);
            await SaveIdeasAsync(ideas);
            return idea;
        }

        public async Task<bool> UpdateIdeaAsync(Idea idea)
        {
            var ideas = await GetAllIdeasAsync();
            var index = ideas.FindIndex(i => i.Id == idea.Id);
            if (index >= 0)
            {
                ideas[index] = idea;
                await SaveIdeasAsync(ideas);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteIdeaAsync(string id)
        {
            var ideas = await GetAllIdeasAsync();
            var ideaToRemove = ideas.FirstOrDefault(i => i.Id == id);
            if (ideaToRemove != null)
            {
                ideas.Remove(ideaToRemove);
                await SaveIdeasAsync(ideas);
                return true;
            }
            return false;
        }

        private async Task SaveIdeasAsync(List<Idea> ideas)
        {
            var ideasFile = Path.Combine(_dataDirectory, "ideas.json");
            var json = JsonSerializer.Serialize(ideas, _jsonOptions);
            await File.WriteAllTextAsync(ideasFile, json);
        }

        // Events CRUD operations
        public async Task<List<Event>> GetAllEventsAsync()
        {
            var eventsFile = Path.Combine(_dataDirectory, "events.json");
            if (!File.Exists(eventsFile))
            {
                return new List<Event>();
            }

            var json = await File.ReadAllTextAsync(eventsFile);
            return JsonSerializer.Deserialize<List<Event>>(json, _jsonOptions) ?? new List<Event>();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            var events = await GetAllEventsAsync();
            return events.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Event> CreateEventAsync(Event eventItem)
        {
            var events = await GetAllEventsAsync();
            events.Add(eventItem);
            await SaveEventsAsync(events);
            return eventItem;
        }

        public async Task<bool> UpdateEventAsync(Event eventItem)
        {
            var events = await GetAllEventsAsync();
            var index = events.FindIndex(e => e.Id == eventItem.Id);
            if (index >= 0)
            {
                events[index] = eventItem;
                await SaveEventsAsync(events);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var events = await GetAllEventsAsync();
            var eventToRemove = events.FirstOrDefault(e => e.Id == id);
            if (eventToRemove != null)
            {
                events.Remove(eventToRemove);
                await SaveEventsAsync(events);
                return true;
            }
            return false;
        }

        private async Task SaveEventsAsync(List<Event> events)
        {
            var eventsFile = Path.Combine(_dataDirectory, "events.json");
            var json = JsonSerializer.Serialize(events, _jsonOptions);
            await File.WriteAllTextAsync(eventsFile, json);
        }

        // Users CRUD operations
        public async Task<List<UserProfile>> GetAllUsersAsync()
        {
            var usersFile = Path.Combine(_dataDirectory, "users.json");
            if (!File.Exists(usersFile))
            {
                return new List<UserProfile>();
            }

            var json = await File.ReadAllTextAsync(usersFile);
            return JsonSerializer.Deserialize<List<UserProfile>>(json, _jsonOptions) ?? new List<UserProfile>();
        }

        public async Task<UserProfile?> GetUserByIdAsync(string id)
        {
            var users = await GetAllUsersAsync();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<UserProfile?> GetUserByUsernameAsync(string username)
        {
            var users = await GetAllUsersAsync();
            return users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<UserProfile> CreateUserAsync(UserProfile user)
        {
            var users = await GetAllUsersAsync();
            users.Add(user);
            await SaveUsersAsync(users);
            return user;
        }

        public async Task<bool> UpdateUserAsync(UserProfile user)
        {
            var users = await GetAllUsersAsync();
            var index = users.FindIndex(u => u.Id == user.Id);
            if (index >= 0)
            {
                users[index] = user;
                await SaveUsersAsync(users);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var users = await GetAllUsersAsync();
            var userToRemove = users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                await SaveUsersAsync(users);
                return true;
            }
            return false;
        }

        private async Task SaveUsersAsync(List<UserProfile> users)
        {
            var usersFile = Path.Combine(_dataDirectory, "users.json");
            var json = JsonSerializer.Serialize(users, _jsonOptions);
            await File.WriteAllTextAsync(usersFile, json);
        }

        public async Task<UserProfile?> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

        // Initialize with sample data if files don't exist
        public async Task InitializeSampleDataAsync()
        {
            var ideasFile = Path.Combine(_dataDirectory, "ideas.json");
            var eventsFile = Path.Combine(_dataDirectory, "events.json");

            if (!File.Exists(ideasFile))
            {
                var sampleIdeas = new List<Idea>
                {
                    new Idea
                    {
                        Title = "Stop-Motion Sculpture",
                        Description = "Animates handcrafted 3D figures through frame-by-frame photography to create the illusion of motion.",
                        Course = "CCIS",
                        Author = "Zeus Awyan",
                        CreatedDate = new DateTime(2024, 1, 15),
                        Likes = 15,
                        Volunteers = 2,
                        EndorsedBy = "Dr. Williams"
                    },
                    new Idea
                    {
                        Title = "Building Diorama",
                        Description = "Miniature 3D model depicting architectural structures or urban scenes in a sealed-down environment.",
                        Course = "CAS",
                        Author = "Maria Santos",
                        CreatedDate = new DateTime(2024, 1, 14),
                        Likes = 11,
                        Volunteers = 5
                    },
                    new Idea
                    {
                        Title = "Pipe Cleaner Art Paintings",
                        Description = "Vibrant, tactile compositions where fuzzy wires are twisted, bent, and layered onto canvas for unique artworks.",
                        Course = "CEA",
                        Author = "Alex Chen",
                        CreatedDate = new DateTime(2024, 1, 13),
                        Likes = 5,
                        Volunteers = 3,
                        EndorsedBy = "Prof. Johnson"
                    },
                    new Idea
                    {
                        Title = "Community Garden Project",
                        Description = "Sustainable gardening initiative to create a green space for students to grow vegetables and learn about environmental responsibility.",
                        Course = "CHS",
                        Author = "Sarah Kim",
                        CreatedDate = new DateTime(2024, 1, 12),
                        Likes = 8,
                        Volunteers = 6,
                        EndorsedBy = "Dr. Green"
                    }
                };
                await SaveIdeasAsync(sampleIdeas);
            }

            if (!File.Exists(eventsFile))
            {
                var sampleEvents = new List<Event>
                {
                    new Event
                    {
                        Title = "Programming Workshop Series",
                        Description = "Weekly workshops covering different programming languages and frameworks",
                        Author = "Zeus Awyan",
                        CreatedDate = new DateTime(2024, 1, 15),
                        ProposedDate = new DateTime(2024, 2, 15),
                        ExpectedParticipants = 30,
                        Likes = 15,
                        Status = EventStatus.Approved,
                        EndorsedBy = "Dr. Williams"
                    },
                    new Event
                    {
                        Title = "Career Development Seminar",
                        Description = "Professional development workshop to help students prepare for their future careers",
                        Author = "Maria Santos",
                        CreatedDate = new DateTime(2024, 1, 14),
                        ProposedDate = new DateTime(2024, 3, 1),
                        ExpectedParticipants = 50,
                        Likes = 11,
                        Status = EventStatus.Planned,
                        EndorsedBy = "Prof. Johnson"
                    },
                    new Event
                    {
                        Title = "Environmental Cleanup Day",
                        Description = "Community initiative to clean up campus and surrounding areas for environmental awareness",
                        Author = "Alex Chen",
                        CreatedDate = new DateTime(2024, 1, 13),
                        ProposedDate = new DateTime(2024, 4, 22),
                        ExpectedParticipants = 25,
                        Likes = 8,
                        Status = EventStatus.UnderReview
                    },
                    new Event
                    {
                        Title = "Gaming Tournament",
                        Description = "Competitive gaming event for students to showcase their skills and build community",
                        Author = "Sarah Kim",
                        CreatedDate = new DateTime(2024, 1, 12),
                        ProposedDate = new DateTime(2024, 2, 28),
                        ExpectedParticipants = 40,
                        Likes = 5,
                        Status = EventStatus.Rejected
                    }
                };
                await SaveEventsAsync(sampleEvents);
            }
        }
    }
}

