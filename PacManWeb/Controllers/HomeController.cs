using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PacManWeb.Models;

namespace PacManWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameServies gameServies;
        public HomeController(GameServies gameServies)
        {
            this.gameServies = gameServies;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
