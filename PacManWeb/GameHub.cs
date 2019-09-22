using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using PacManLibrary;
using PacManWeb.Models;
using System.Collections.Generic;
using System;

namespace PacManWeb
{
    public class GameHub : Hub
    {
        private readonly Game newGame;
        private Game game;
        private Guid guid;

        public GameHub(IGameServies gameServies, Game newGame)
        {
            this.newGame = newGame;
            games = gameServies.Games;
        }
        private readonly Dictionary<Guid, Game> games;
        
        public async Task RequestData(string id)
        {
            if (id == "0" || id == null)
            {
                var game = newGame;
                var guid = Guid.NewGuid();
                this.guid = guid;
                games.Add(guid, game);
                this.game = game;
            }
            else
            {
                try
                {
                    guid = new Guid(id);
                    game = games[guid];
                }
                catch (Exception)
                {
                    throw;
                }
            }

            var map = new string[game.Border.GetLength(0), game.Border.GetLength(1)];
            for (int i = 0; i < game.Border.GetLength(0); i++)
            {
                for (int j = 0; j < game.Border.GetLength(1); j++)
                {
                    map[i, j] = game.Border[i, j].ToString();
                }
            }
            var mo = new List<MovebleObject>
            {
                new MovebleObject(game.PacMan)
            };
            foreach (var item in game.Enemies)
            {
                mo.Add(new MovebleObject(item));
            }
            var gameState = new ReturnData
            {
                Direction = (int)game.PacMan.Direction,
                Lives = game.PacMan.Life,
                Map = map,
                Score = game.PacMan.Score,
                GameId = guid,
                GameStat = game.GameStat,
                MovebleObjects=mo,
                X = game.PacMan.X,
                Y = game.PacMan.Y
            };
            await Clients.Caller.SendAsync("ReceiveData", gameState);
        }
        public void Restar(string id)
        {
            SelectGame(id);
            game.Restart();
        }

        public void MoveUp(string id)
        {
            SelectGame(id);
            game.MoveUp();
        }
        public void MoveDown(string id)
        {
            SelectGame(id);
            game.MoveDown();
        }
        public void MoveLeft(string id)
        {
            SelectGame(id);
            game.MoveLeft();
        }
        public void MoveRight(string id)
        {
            SelectGame(id);
            game.MoveRight();
        }
        private void SelectGame(string id)
        {
            try
            {
                guid = new Guid(id);
                game = games[guid];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}