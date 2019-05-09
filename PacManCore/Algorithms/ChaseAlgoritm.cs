using PacManLibrary.Interfaces;
using System;
using System.Collections.Generic;
using PacManLibrary.Algorithms.A_star;

namespace PacManLibrary.Algorithms
{
    public class ChaseAlgoritm : IAlgorithm
    {
        readonly PathFinder pathFinder;
        readonly int[,] rawMap;
        const int N = 36;
        const int M = 28;
        public ChaseAlgoritm()
        {
            rawMap = new int[36, 28];

            pathFinder = new PathFinder();
        }
        public Direction GetDirection(IPoint[,] map, IPoint from, IPoint destination)
        {
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (map[i, j] is IWall wall)
                    {
                        rawMap[i, j] = 0;
                    }
                    else
                    {
                        rawMap[i, j] = 1;
                    }
                }
            }
            if (from is IEnemy item)
            {
                switch (item.Direction)
                {
                    case Direction.Right:
                        rawMap[item.X, item.Y - 1] = 0;
                        break;
                    case Direction.Down:
                        rawMap[item.X - 1, item.Y] = 0;
                        break;
                    case Direction.Up:
                        rawMap[item.X + 1, item.Y] = 0;
                        break;
                    case Direction.Left:
                        rawMap[item.X, item.Y + 1] = 0;
                        break;
                }
            }
            List<Point> path = pathFinder.FindPath(rawMap, new Point(from.X, from.Y), new Point(destination.X, destination.Y));
            Point nextNode;
            try
            {
                nextNode = path[1];
            }
            catch (Exception)
            {
                return new ChaoticAlgoritm().GetDirection(map, from, destination);
            }
            Direction direction = Direction.Left;
            if (nextNode.X > from.X)
            {
                direction = Direction.Down;
            }
            if (nextNode.X < from.X)
            {
                direction = Direction.Up;
            }
            if (nextNode.Y > from.Y)
            {
                direction = Direction.Right;
            }
            if (nextNode.Y < from.Y)
            {
                direction = Direction.Left;
            }
            return direction;
        }
        private Point ValidatePoint(IPoint abstractPoint)
        {
            if (abstractPoint.X > N)
            {
                abstractPoint.X = N - 1;
            }
            if (abstractPoint.X < 0)
            {
                abstractPoint.X = 0;
            }
            if (abstractPoint.Y > M)
            {
                abstractPoint.Y = M - 1;
            }
            if (abstractPoint.Y < 0)
            {
                abstractPoint.Y = 0;
            }
            return new Point(abstractPoint.X, abstractPoint.Y);
        }
    }
}
