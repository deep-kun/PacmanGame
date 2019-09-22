﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace PacManLibrary.Algorithms.A_star
{
    class PathFinder
    {
        public List<Point> FindPath(int[,] field, Point start, Point goal)
        {
            int k = 0;
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                k++;
                var currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();
                /////////////////
                if (k == 2)
                {
                    return GetPathForNode(currentNode);
                }
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                      if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }

            return null;
        }
        private int GetDistanceBetweenNeighbours()
        {
            return 1;
        }
        private int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
        private Collection<PathNode> GetNeighbours(PathNode pathNode,
  Point goal, int[,] field)
        {
            var result = new Collection<PathNode>();

            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                if (field[point.X, point.Y] == 0)
                { continue; }
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };
                result.Add(neighbourNode);
            }
            return result;
        }
        private List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
