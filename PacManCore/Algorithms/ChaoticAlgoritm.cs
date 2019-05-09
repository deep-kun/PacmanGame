using System;
using System.Collections.Generic;
using PacManLibrary.Interfaces;

namespace PacManLibrary.Algorithms
{
    public class ChaoticAlgoritm : IAlgorithm
    {
        private IPoint[,] map;
        private IPoint from;
        private IPoint destination;
        private Direction ovalue = Direction.Left;
        private Random random = new Random();

        public Direction GetDirection(IPoint[,] map, IPoint from, IPoint destination)
        {
            this.from = from;
            this.destination = destination;
            ovalue = ((IEnemy)from).Direction;
            this.map = map;
            var value = RandomEnumValue<Direction>();
            value = RandomEnumValue<Direction>();
            while ((int)value == ((int)ovalue * (-1)))
            {
                value = RandomEnumValue<Direction>();
            }
            ovalue = value;
            return value;
        }

        private Direction[] AllowedDestination()
        {
            var list = new List<Direction>();
            if (!(map[from.X + 1, from.Y] is IWall))
            {
                list.Add(Direction.Down);
            }
            if (!(map[from.X - 1, from.Y] is IWall))
            {
                list.Add(Direction.Up);
            }
            if (!(map[from.X, from.Y + 1] is IWall))
            {
                list.Add(Direction.Right);
            }
            if (!(map[from.X, from.Y - 1] is IWall))
            {
                list.Add(Direction.Left);
            }
            return list.ToArray();
        }

        private T RandomEnumValue<T>()
        {
            var v = AllowedDestination();
            return (T)v.GetValue(random.Next(v.Length));
        }

    }
}
