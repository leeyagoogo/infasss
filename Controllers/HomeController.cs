using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using INFASS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace INFASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly List<User> RegisteredUsers = new List<User>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [Route("/getUser")]
        public IActionResult getUser([FromForm] User user)
        {
            if (user == null)
                return BadRequest("No user submitted.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (RegisteredUsers.Any(u => string.Equals(u.Email, user.Email?.Trim(), StringComparison.OrdinalIgnoreCase)))
                return BadRequest("Email already registered.");

            user.Email = user.Email?.Trim() ?? string.Empty;
            RegisteredUsers.Add(user);

            return Content("Controller: " + user.Name + " " + user.Email);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("/loginUser")]
        public IActionResult LoginUser(string email, string pass)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
                return BadRequest("Missing email or password.");

            var match = RegisteredUsers.FirstOrDefault(u =>
                string.Equals(u.Email, email.Trim(), StringComparison.OrdinalIgnoreCase)
                && u.Password == pass);

            if (match != null)
                return Json(new { success = true });

            return BadRequest("Invalid email or password.");
        }

        [HttpGet]
        public IActionResult Register() => View();

        public IActionResult Privacy() => View();

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