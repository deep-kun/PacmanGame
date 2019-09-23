using System.Collections.Generic;
using PacManLibrary.Interfaces;
using PacManLibrary.Model;

namespace PacManLibrary
{
    public interface IGame
    {
        IPoint[,] Border { get; }
        List<IEnemy> Enemies { get; }
        IGameContext GameContext { get; set; }
        GameStat GameStat { get; }
        PacMan PacMan { get; }

        void Logger(string lines);
        void Restart();
        void StartLoop();
    }
}