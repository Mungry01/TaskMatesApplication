using Microsoft.AspNetCore.Mvc;
using TaskMates.Models;
using TaskMates.Services;

namespace TaskMates.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ApiController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Ideas Endpoints
        [HttpGet("ideas")]
        public async Task<IActionResult> GetIdeas()
        {
            var ideas = await _dataService.GetAllIdeasAsync();
            return Ok(ideas);
        }

        [HttpGet("ideas/{id}")]
        public async Task<IActionResult> GetIdea(string id)
        {
            var idea = await _dataService.GetIdeaByIdAsync(id);
            if (idea == null) return NotFound();
            
            var comments = await _dataService.GetCommentsForItemAsync(id);
            return Ok(new { idea, comments });
        }

        [HttpPost("ideas")]
        public async Task<IActionResult> CreateIdea([FromBody] Idea idea)
        {
            var created = await _dataService.CreateIdeaAsync(idea);
            return Ok(created);
        }

        [HttpPost("ideas/{id}/like")]
        public async Task<IActionResult> LikeIdea(string id, [FromBody] LikeRequest request)
        {
            var success = await _dataService.LikeIdeaAsync(id, request.UserName);
            if (success)
            {
                var idea = await _dataService.GetIdeaByIdAsync(id);
                return Ok(new { likes = idea?.Likes });
            }
            return BadRequest();
        }

        [HttpPost("ideas/{id}/volunteer")]
        public async Task<IActionResult> VolunteerForIdea(string id, [FromBody] VolunteerRequest request)
        {
            var success = await _dataService.VolunteerForIdeaAsync(id, request.UserName);
            if (success)
            {
                var idea = await _dataService.GetIdeaByIdAsync(id);
                return Ok(new { volunteers = idea?.Volunteers });
            }
            return BadRequest();
        }

        [HttpPost("ideas/{id}/endorse")]
        public async Task<IActionResult> EndorseIdea(string id, [FromBody] EndorseRequest request)
        {
            var success = await _dataService.EndorseIdeaAsync(id, request.FacultyName);
            if (success)
            {
                var idea = await _dataService.GetIdeaByIdAsync(id);
                return Ok(idea);
            }
            return BadRequest();
        }

        [HttpDelete("ideas/{id}")]
        public async Task<IActionResult> DeleteIdea(string id)
        {
            var success = await _dataService.DeleteIdeaAsync(id);
            return success ? Ok() : NotFound();
        }

        // Events Endpoints
        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _dataService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetEvent(string id)
        {
            var eventItem = await _dataService.GetEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            
            var comments = await _dataService.GetCommentsForItemAsync(id);
            return Ok(new { eventItem, comments });
        }

        [HttpPost("events")]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventItem)
        {
            var created = await _dataService.CreateEventAsync(eventItem);
            return Ok(created);
        }

        [HttpPost("events/{id}/like")]
        public async Task<IActionResult> LikeEvent(string id, [FromBody] LikeRequest request)
        {
            var success = await _dataService.LikeEventAsync(id, request.UserName);
            if (success)
            {
                var eventItem = await _dataService.GetEventByIdAsync(id);
                return Ok(new { likes = eventItem?.Likes });
            }
            return BadRequest();
        }

        [HttpPost("events/{id}/status")]
        public async Task<IActionResult> UpdateEventStatus(string id, [FromBody] StatusUpdateRequest request)
        {
            var success = await _dataService.UpdateEventStatusAsync(id, request.Status);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var success = await _dataService.DeleteEventAsync(id);
            return success ? Ok() : NotFound();
        }

        // Comments Endpoints
        [HttpPost("comments")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            var created = await _dataService.CreateCommentAsync(comment);
            return Ok(created);
        }

        [HttpGet("comments/{itemId}")]
        public async Task<IActionResult> GetComments(string itemId)
        {
            var comments = await _dataService.GetCommentsForItemAsync(itemId);
            return Ok(comments);
        }

        // User Profile Endpoints
        [HttpGet("profile/{userName}")]
        public async Task<IActionResult> GetProfile(string userName)
        {
            var profile = await _dataService.GetUserProfileAsync(userName);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
    }

    // Request Models
    public class LikeRequest
    {
        public string UserName { get; set; } = string.Empty;
    }

    public class VolunteerRequest
    {
        public string UserName { get; set; } = string.Empty;
    }

    public class EndorseRequest
    {
        public string FacultyName { get; set; } = string.Empty;
    }

    public class StatusUpdateRequest
    {
        public EventStatus Status { get; set; }
    }
}

