using System.Diagnostics;
using INFASS.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static List<User> users = new List<User>
        {
            new User
            {
                Username = "admin",
                Email = "admin@infass.com",
                Password = "1234"
            }
        };


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var account = users.FirstOrDefault(u =>
                u.Username == user.Username &&
                u.Password == user.Password);

            if (account != null)
            {
                TempData["Success"] = "Login Successful!";
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid username or password.";
            return View(user);
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            // Check if username already exists
            if (users.Any(u => u.Username == user.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View(user);
            }

            // Check passwords
            if (user.Password != user.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View(user);
            }

            // Save user temporarily
            users.Add(new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            });

            TempData["Success"] = "Registration successful! Please login.";

            return RedirectToAction("Login");
        }

        // ================= OTHER =================

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}