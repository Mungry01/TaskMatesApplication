using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskMates.Models;
using TaskMates.Services;
using System.Text.Json;

namespace TaskMates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileStorageService _fileStorage;

        public HomeController(ILogger<HomeController> logger, FileStorageService fileStorage)
        {
            _logger = logger;
            _fileStorage = fileStorage;
        }

        // Role Selection Page - First page users see
        public IActionResult RoleSelection()
        {
            return View();
        }

        // Login GET - Show login form
        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect to dashboard
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Login POST - Handle login submission
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string role)
        {
            var user = await _fileStorage.ValidateUserAsync(username, password);
            
            if (user != null && user.Role == role)
            {
                // Store user info in session
                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserEmail", user.Email);
                
                return RedirectToAction("Index");
            }
            
            TempData["ErrorMessage"] = "Invalid username or password, or incorrect role.";
            return View();
        }

        // Register GET - Show registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register POST - Handle registration submission
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string confirmPassword, 
                                                   string name, string email, string position, string role)
        {
            // Validate passwords match
            if (password != confirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return View();
            }

            // Check if username already exists
            var existingUser = await _fileStorage.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Username already exists. Please choose another.";
                return View();
            }

            // Create new user
            var newUser = new UserProfile
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = password, // In production, hash this!
                Name = name,
                Email = email,
                Position = position,
                Role = role,
                DateJoined = DateTime.Now
            };

            await _fileStorage.CreateUserAsync(newUser);
            
            TempData["SuccessMessage"] = "Account created successfully! Please login.";
            return RedirectToAction("Login");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("RoleSelection");
        }

        // Dashboard/Home - Requires authentication
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("RoleSelection");
            }
            return View();
        }

        public IActionResult About()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("RoleSelection");
            }
            return View();
        }

        public IActionResult Ideas()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("RoleSelection");
            }
            return View();
        }

        public IActionResult Events()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("RoleSelection");
            }
            return View();
        }

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("RoleSelection");
            }
            return View();
        }

        // Keep old PseudoLogin/Register for backwards compatibility (redirect to new pages)
        public IActionResult PseudoLogin()
        {
            return RedirectToAction("RoleSelection");
        }

        public IActionResult PseudoRegister()
        {
            return RedirectToAction("Register");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
