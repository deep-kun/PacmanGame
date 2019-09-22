using System.Collections.Generic;
using PacManLibrary.Interfaces;

namespace PacManLibrary
{
    public interface IGameContext
    {
        List<IDisappearable> Disappearables { get; set; }
        List<IEnemy> Enemies { get; set; }
        IEventSink EventSink { get; set; }
        bool ISWin { get; set; }
        IPoint[,] Map { get; set; }
        ICharacter PacMan { get; set; }
    }
}