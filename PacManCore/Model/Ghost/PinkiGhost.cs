using PacManLibrary.Events;
using System.Timers;
using PacManLibrary.Algorithms;

namespace PacManLibrary.Model
{
    public class PinkiGhost : GhostAbstract
    {
        private const int homeX = 4;
        private int normalSpeed = 3;
        private const int homeY = 1;

        public PinkiGhost(IGameContext context)
            : base(context)
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
            get
            {
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
            const int corVal = 4;
            if (character.Direction == Direction.Right)
            {
                return new AbstractPoint(character.X, character.Y + corVal);
            }
            if (character.Direction == Direction.Left)
            {
                return new AbstractPoint(character.X, character.Y - corVal);
            }
            if (character.Direction == Direction.Up)
            {
                return new AbstractPoint(character.X + corVal, character.Y);
            }
            if (character.Direction == Direction.Down)
            {
                return new AbstractPoint(character.X - corVal, character.Y);
            }
            return new AbstractPoint(character.X, character.Y);
        }
    }
}
