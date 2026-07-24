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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/getUser")]
        public IActionResult GetUser(string name, string email, string pass)
        {
            User usr = new User();

            string[] values = { "Name", "Email", "Password" };
            string[] numbers = { name, email, pass };


            return Content(usr.sql(values, numbers));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[Route("/loginUser")]
        //public IActionResult LoginUser(string email, string pass)
        //{

        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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