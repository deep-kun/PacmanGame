using PacManLibrary.Interfaces;
using PacManLibrary.Events;
using PacManLibrary.Algorithms;
using System.Timers;

namespace PacManLibrary.Model
{
    public class BlinkiGhost : GhostAbstract
    {
        private const int homeX = 4;
        private const int homeY = 26;
        private int normalSpeed = 2;

        public BlinkiGhost(IGameContext context)
            :base(context)
        {
            HomePoint.X = homeX;
            HomePoint.Y = homeY;
            eventSink.Subscribe<EnergizerEatedEvent>(this.Scare);
            algorithm = chaoticAlgoritm;
            timer = new Timer(timeToChase);
            direction = Direction.Left;
            timer.Start();
            timer.Elapsed += StartChase;
        }
        
        public override int Velocity
        {
            get {                
                if (algorithm is HomeAlgoritm)
                {
                    return speedScared;
                }
                if (Dying)
                {
                    return speedDead;
                }
                return normalSpeed;
            }
            set => normalSpeed = value;
        }
        public override AbstractPoint Correcting()
        {
            var t = new AbstractPoint(character.X, character.Y);
            return t;
        }
    }
}
