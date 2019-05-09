using PacManLibrary.Interfaces;
using System.Collections.Generic;

namespace PacManLibrary
{
    public class GameContext
    {
        public IPoint[,] Map { get; set; }
        public ICharacter PacMan { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public List<IDisappearable> Disappearables { get; set; }
        public IEventSink EventSink { get; set; }
        public bool ISWin { get; set; } = false;
    }
}
