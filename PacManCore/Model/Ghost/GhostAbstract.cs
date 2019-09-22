using System;
using PacManLibrary.Events;
using PacManLibrary.Interfaces;
using PacManLibrary.Algorithms;
using System.Timers;

namespace PacManLibrary.Model
{
    public abstract class GhostAbstract : IEnemy
    {
        private AbstractPoint birthPlase = new AbstractPoint(14, 14);
        private double x = 0;
        private double y = 0;
        private int moves = 0;
        private readonly double k = 0.2;
        protected int speedScared = 20;
        protected int speedDead = 0;
        protected IPoint[,] map;
        protected ICharacter character;
        protected IAlgorithm algorithm;
        protected Direction direction;
        protected IEventSink eventSink;
        protected bool onGhost = false;
        protected Timer timer;
        protected ChaseAlgoritm chaseAlgoritm = new ChaseAlgoritm();
        protected ChaoticAlgoritm chaoticAlgoritm = new ChaoticAlgoritm();
        protected int timeToChase = 7000;
        protected int timeToSpin = 7000;

        public GhostAbstract(IGameContext context)
        {
            this.character = context.PacMan;
            this.map = context.Map;
            this.eventSink = context.EventSink;
        }

        public double R { get; } = 0.8;
        public int X { get; set; }
        public int Y { get; set; }
        public double Xpixel => x + X;
        public double Ypixel => y + Y;
        public abstract int Velocity { get; set; }
        public AbstractPoint HomePoint { get; protected set; } = new AbstractPoint();
        public bool Scared { get; protected set; }
        public bool Dying { get; set; }
        public bool Dead { get; set; } = false;
        public IPoint OnPoint { get; set; } = new EatedFood();
        public float ConsumptionValue => 200;
        public Direction Direction => direction;

        public bool Hiden { get; set; } = false;

        public abstract AbstractPoint Correcting();

        public void Move()
        {
            if (Hiden)
            {
                return;
            }
            if (moves < Velocity)
            {
                moves++;
                return;
            }
            moves = 0;
            map[X, Y] = OnPoint;
            if (X == birthPlase.X && Y == birthPlase.Y && Dying)
            {
                Respawn();
                return;
            }

            if (OnPoint is IPacMan p)
            {
                ColisionDetected(p);
                return;
            }
            if (OnPoint is IEnemy g1)
            {
                onGhost = false;
                g1.Hiden = false;

                var point = g1.OnPoint;
                while (point is IEnemy ng)
                {
                    point = ng.OnPoint;
                }

                if (point is IChoosable)
                {
                    CheckDirection();
                }
            }
            if (X == 17 && Y == 0)
            {
                Y = 26;
                y = 1;
                map[X, Y] = this;
                direction = Direction.Left;
                return;
            }
            if (X == 17 && Y == 27)
            {
                Y = 1;
                y = -1;
                map[X, Y] = this;
                direction = Direction.Right;
                return;
            }
            bool any = false;
            if (this.direction == Direction.Down && !(map[X + 1, Y] is Wall) && !(map[X + 1, Y] is Wall))
            {
                x += k;
                if (x > 1)
                {
                    OnPoint = map[X + 1, Y];
                    X++;
                    x = 0;
                    SheerMove();
                }
                any = true;
            }
            if (this.direction == Direction.Up && !(map[X - 1, Y] is Wall) && !(map[X - 1, Y] is Wall))
            {
                x -= k;
                if (-1 == x)
                {
                    OnPoint = map[X - 1, Y];
                    X--;
                    x = 0;
                    SheerMove();
                }
                any = true;
            }
            if (this.direction == Direction.Right && !(map[X, Y + 1] is Wall) && !(map[X, Y + 1] is Wall))
            {
                y += k;
                if (y > 1)
                {
                    OnPoint = map[X, Y + 1];
                    Y++;
                    y = 0;
                    SheerMove();
                }
                any = true;
            }
            if (this.direction == Direction.Left && !(map[X, Y - 1] is Wall) && !(map[X, Y - 1] is Wall))
            {
                y -= k;
                if (-1 == y)
                {
                    OnPoint = map[X, Y - 1];
                    Y--;
                    y = 0;
                    SheerMove();
                }
                any = true;
            }
            if ((any == false) && y == 0 && x == 0)
            {
                SheerMove();
            }
            map[X, Y] = this;
            if (onGhost)
            {
                ((IEnemy)OnPoint).Hiden = false;
            }
            if (OnPoint is IEnemy g)
            {
                onGhost = true;
                g.Hiden = true;
            }
        }

        public void ReInitializate()
        {
            Dead = false;
            Dying = false;
            OnPoint = new EatedFood();
            Scared = false;
            algorithm = chaoticAlgoritm;
            timer = new Timer(timeToChase);
            direction = Direction.Left;
            timer.Start();
            timer.Elapsed += StartChase;
        }

        protected void StartChase(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            timer = new Timer(timeToSpin);
            algorithm = chaseAlgoritm;
            InvertDirection();
            timer.Elapsed += TimerToSpin_Elapsed;
        }

        public void Kill()
        {
            timer.Stop();
            Dying = true;
            algorithm = chaseAlgoritm;
        }

        public void Respawn()
        {
            if (map[X, Y] is IEnemy)
            {
                return;
            }
            Dead = false;
            X = birthPlase.X;
            Y = birthPlase.Y;
            ReInitializate();
            map[X, Y] = this;
        }

        protected void CheckDirection()
        {
            if (!this.AvalaibleX() && this.AvalaibleY())
            {
                return;
            }
            if (Dying)
            {
                direction = algorithm.GetDirection(map, this, birthPlase);
                return;
            }

            if (algorithm is ChaseAlgoritm)
            {
                try
                {
                    var cp = Correcting();
                    direction = algorithm.GetDirection(map, this, cp);

                }
                catch (Exception e)
                {
                    Logger(e.ToString());
                }
                return;
            }
            direction = algorithm.GetDirection(map, this, character);
        }

        private void ColisionDetected(IPacMan pacman)
        {
            OnPoint = new EatedFood { IsChoosable = pacman.OnWayChosser };
            pacman.Eat(this);
            map[X, Y] = this;
        }

        public override string ToString()
        {
            if (Dying)
            {
                return "deadGhost";
            }
            if (Scared)
            {
                return "frightenedGhost";
            }
            return this.GetType().Name;
        }

        protected void Scare(EnergizerEatedEvent energizerEatedEvent)
        {
            timer.Stop();
            Scared = true;
            algorithm = new HomeAlgoritm(chaseAlgoritm, HomePoint);
            timer = new Timer(timeToSpin);
            timer.Start();
            timer.Elapsed += TimerToSpin_Elapsed;
        }

        protected void TimerToSpin_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Scared = false;
            algorithm = chaoticAlgoritm;
            timer = new Timer(timeToChase);
            timer.Start();
            timer.Elapsed += StartChase;
        }

        protected void SheerMove()
        {
            if (OnPoint is IChoosable choosable && choosable.IsChoosable)
            {
                CheckDirection();
            }
        }

        protected void InvertDirection()
        {
            var tempDir = direction;
            direction = (Direction)((-1) * (int)direction);

            if (direction == Direction.Down && (map[X + 1, Y] is Wall))
            {
                direction = tempDir;
            }

            if (direction == Direction.Up && (map[X - 1, Y] is Wall))
            {
                direction = tempDir;
            }

            if (direction == Direction.Right && (map[X, Y + 1] is Wall))
            {
                direction = tempDir;
            }

            if (direction == Direction.Left && (map[X, Y - 1] is Wall))
            {
                direction = tempDir;
            }
        }

        public void Logger(String lines)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\deep\source\repos\PacMan\PacManLibrary\Stuff\logs.txt", true);
            file.WriteLine(DateTime.Now.ToString() + " " + lines);
            file.Close();
        }
    }
}
