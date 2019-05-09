using PacManLibrary.Interfaces;
using PacManLibrary.Events;
using PacManLibrary.Algorithms;
using System.Timers;
using System;

namespace PacManLibrary.Model
{
    public class ClaydGhost : GhostAbstract
    {
        private const int homeX = 32;
        private const int homeY = 1;
        private int normalSpeed = 3;


        public ClaydGhost(GameContext context)
            : base(context)
        {
            HomePoint.X = homeX;
            HomePoint.Y = homeY;
            chaoticAlgoritm = new ChaoticAlgoritm();
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
            double d = Math.Sqrt(Math.Pow(character.X - this.X, 2) + Math.Pow(character.X - this.X, 2));
            int ad = Math.Abs((int)d);
            if (ad > 8)
            {
                return new AbstractPoint(character.X, character.Y);
            }
            return new AbstractPoint(homeX, homeY);
            //return new AbstractPoint(character.X, character.Y);
        }
    }
}
