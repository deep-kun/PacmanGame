using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PacaManDataAccessLayer;
using PacaManDataAccessLayer.Model;
using PacManWeb.Models;

namespace PacManWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameServies gameServies;
        private readonly IHighScoreRepository scoreRepository;

        public HomeController(IGameServies gameServies, IHighScoreRepository scoreRepository)
        {
            this.gameServies = gameServies;
            this.scoreRepository = scoreRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(this.Game));
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

        public IActionResult RecordScore(Guid id)
        {
            return View(new HighScoreViewModel { Id = id });
        }

        public IActionResult HighScores()
        {
            return View(this.scoreRepository.GetTopHighScore().Result);
        }

        [HttpPost]
        public IActionResult RecordScore(HighScoreViewModel highScore)
        {   
            if (!ModelState.IsValid)
            {
                return View(new HighScoreViewModel { Id = highScore.Id });
            }

            if(!gameServies.Games.TryGetValue(highScore.Id, out var game) || game is null)
            {
                return Error();
            }

            var hsc = new HighScore
            {
                GameId = highScore.Id,
                Name = highScore.Name,
                Date = DateTime.Now,
                Score = game.PacMan.Score
            };
            scoreRepository.AddHighScore(hsc);
            return RedirectToAction("Highscores");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
