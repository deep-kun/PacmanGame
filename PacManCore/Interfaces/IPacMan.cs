using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManLibrary.Interfaces
{
    public interface IPacMan : ICharacter
    {
        bool OnWayChosser { get; } 
        void Eat(IPoint point);
        void SetDirection(Direction direction);
        bool ColisionDetect();
    }
}
