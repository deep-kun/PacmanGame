using PacManLibrary.Interfaces;
using PacManLibrary.Events;
using PacManLibrary.Algorithms;
using System.Timers;
using System;

namespace PacManLibrary.Model
{
    public class InkiGhost : GhostAbstract
    {
        private const int homeX = 32;
        private const int homeY = 26;
        private readonly IEnemy blinki;
        private int normalSpeed = 3;
        
        public InkiGhost(IGameContext context)
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
            blinki = context.Enemies.Find(t => t is BlinkiGhost);
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

            int deltaX = Math.Abs(character.X - blinki.X);
            int deltaY = Math.Abs(character.Y - blinki.Y);
            int dstX = 0;
            int dstY = 0;
            if (character.Direction == Direction.Right)
            {
                dstX = character.X;
                dstY = character.Y + deltaY;
            }
            if (character.Direction == Direction.Left)
            {
                dstX = character.X;
                dstY = character.Y - deltaY;
            }
            if (character.Direction == Direction.Up)
            {
                dstX = character.X + deltaY;
                dstY = character.Y;
            }
            if (character.Direction == Direction.Down)
            {
                dstX = character.X - deltaY;
                dstY = character.Y;
            }
            return new AbstractPoint(dstX, dstY);
        }
    }
}
