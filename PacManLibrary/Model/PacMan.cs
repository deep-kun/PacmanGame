using PacManLibrary.Interfaces;
using PacManLibrary.Events;
using System.Collections.Generic;

namespace PacManLibrary.Model
{
    public class PacMan : IPacMan
    {
        private const int thirtyPoint = 30;
        private const int sevebtyPoint = 70;
        private const int eightyPoint = 80;
        private const double k = 0.2;
        private double x = 0;
        private double y = 0;
        private int moves = 0;
        private int dropMutlt = 0;
        private readonly IPoint[,] map;
        private Direction badDirection;
        private int comboMutiplayer = 1;
        private bool bd = false;

        public PacMan(IPoint[,] map, IEventSink eventSink)
        {
            this.Direction = Direction.Left;
            this.EventSink = eventSink;
            this.map = map;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public float Score { get; set; }
        public bool IsLive { get; set; } = true;
        public int Life { get; set; }
        public Direction Direction { get; private set; }
        public bool OnWayChosser { get; private set; } = false;
        public bool OnDeadGhost { get; set; } = false;
        public IPoint DeadGhostRef { get; set; }
        public IEventSink EventSink { get; set; }
        public double Xpixel { get => x + X; set => x = value; }
        public double Ypixel { get => y + Y; set => y = value; }
        public double R { get; } = 0.8;
        public int Velocity { get; set; } = 3;


        public void Eat(IPoint point)
        {
            if (OnDeadGhost)
            {
                return;
            }
            dropMutlt--;
            if (dropMutlt < 0) { comboMutiplayer = 1; }
            if (point is IBehaviorChangeble changeble)
            {
                if (changeble is IEtable etable)
                {
                    Score += etable.ConsumptionValue;
                    EventSink.Publish(new EnergizerEatedEvent());
                }
                return;
            }
            if (point is GhostAbstract enemy)
            {
                if (enemy.Scared)
                {
                    Score += comboMutiplayer * enemy.ConsumptionValue;
                    comboMutiplayer *= 2;
                    dropMutlt = 10;
                    enemy.Kill();
                    OnDeadGhost = true;
                    DeadGhostRef = enemy;
                    return;
                }
                else
                {
                    this.Life--;
                    this.IsLive = false;
                    return;
                }
            }
            if (point is IEtable food)
            {
                if (food is BaseFood)
                {
                    EventSink.Publish(new FoodEated());
                }
                Score += food.ConsumptionValue;
            }
        }
        public void Move()
        {
            #region Velocity
            if (moves < Velocity)
            {
                moves++;
                return;
            }
            moves = 0;
            #endregion

            map[X, Y] = new EatedFood { X = X, Y = Y, IsChoosable = OnWayChosser };
            if (X == 23 && Y == 1)
            {

            }
            if (X == 17 && Y == 0)
            {
                Y = 26;
                y = 1 - k;
                map[X, Y] = this;
                Direction = Direction.Left;
                if (bd)
                {
                    SetDirection(badDirection);
                }
                return;
            }
            if (X == 17 && Y == 27)
            {
                Y = 1;
                y = -1 + k;
                map[X, Y] = this;
                Direction = Direction.Right;
                if (bd)
                {
                    SetDirection(badDirection);
                }
                return;
            }


            if (this.Direction == Direction.Down && !(map[X + 1, Y] is Wall))
            {
                x += k;
                if (x > 1)
                {
                    X++;
                    x = 0;
                    SheerMove();
                }
            }

            if (this.Direction == Direction.Up && !(map[X - 1, Y] is Wall))
            {
                x -= k;
                if (-1 == x)
                {
                    X--;
                    x = 0;
                    SheerMove();
                }
            }

            if (this.Direction == Direction.Right && !(map[X, Y + 1] is Wall))
            {
                y += k;
                if (y > 1)
                {
                    Y++;
                    y = 0;
                    SheerMove();
                }
            }

            if (this.Direction == Direction.Left && !(map[X, Y - 1] is Wall))
            {
                y -= k;
                if (-1 == y)
                {
                    Y--;
                    y = 0;
                    SheerMove();
                }
            }

            if (map[X, Y] is IChoosable choosable)
            {
                OnWayChosser = choosable.IsChoosable;
            }
            else
            {
                OnWayChosser = false;
            }
            this.Eat(map[X, Y]);
            ColisionDetect();
            map[X, Y] = this;
            if (bd)
            {
                SetDirection(badDirection);
            }
        }

        public void SetDirection(Direction direction)
        {
            if (direction == Direction.Down && !(map[X + 1, Y] is Wall) && !(map[(int)Xpixel + 1, (int)Ypixel] is Wall) && AvalaibleY())
            {
                bd = false;
                this.Direction = direction;
                return;
            }
            if (direction == Direction.Up && !(map[X - 1, Y] is Wall) && !(map[(int)Xpixel - 1, (int)Ypixel] is Wall) && AvalaibleY())
            {
                bd = false;
                this.Direction = direction;
                return;
            }
            if (direction == Direction.Right && !(map[X, Y + 1] is Wall) && !(map[(int)X, (int)Ypixel + 1] is Wall) && AvalaibleX())
            {
                bd = false;
                this.Direction = direction;
                return;
            }
            if (direction == Direction.Left && !(map[X, Y - 1] is Wall) && !(map[(int)Xpixel, (int)Ypixel - 1] is Wall) && AvalaibleX())
            {
                bd = false;
                this.Direction = direction;
                return;
            }
            bd = true;
            badDirection = direction;
        }

        public bool ColisionDetect()
        {
            var neighbor = new List<IPoint>();
            if (Y + 1 < map.GetLength(1))
            {
                if (!(map[X, Y + 1] is Wall))
                {
                    neighbor.Add(map[X, Y + 1]);
                }
            }
            if (Y - 1 >= 0)
            {
                if (!(map[X, Y - 1] is Wall))
                {
                    neighbor.Add(map[X, Y - 1]);
                }
            }
            if (!(map[X + 1, Y] is Wall))
            {
                neighbor.Add(map[X + 1, Y]);
            }
            if (!(map[X - 1, Y] is Wall))
            {
                neighbor.Add(map[X - 1, Y]);
            }
            foreach (var item in neighbor)
            {
                if (this.ColisionDetecion(item))
                {
                    this.Eat(item);
                    if ((item is IEtable) && !(item is IEnemy))
                    {
                        bool isChoosable = false;
                        if (item is IChoosable choosable)
                            isChoosable = choosable.IsChoosable;
                        map[item.X, item.Y] = new EatedFood() { IsChoosable = isChoosable };
                    }
                }
            }
            return true;
        }

        private void SheerMove()
        {
            OnDeadGhost = false;
        }

        private bool AvalaibleY()
        {
            if (Ypixel % Y == 0)
            {
                return true;
            }
            return false;
        }
        private bool AvalaibleX()
        {
            if (Xpixel % X == 0)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "@";
        }
    }
}
