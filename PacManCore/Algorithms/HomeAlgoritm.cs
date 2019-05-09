using System;
using PacManLibrary.Interfaces;
using PacManLibrary.Model;

namespace PacManLibrary.Algorithms
{
    public class HomeAlgoritm : IAlgorithm
    {
        private IAlgorithm algorithm;
        private readonly IPoint homepoint;
        public HomeAlgoritm(IAlgorithm algorithm, IPoint Homepoint)
        {
            this.algorithm = algorithm;
            homepoint = Homepoint;
        }

        public Direction GetDirection(IPoint[,] map, IPoint from, IPoint destination)
        {
            return algorithm.GetDirection(map, from, new AbstractPoint(homepoint.X, homepoint.Y));
        }
        public void Logger(String lines)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\deep\source\repos\PacMan\PacManLibrary\Stuff\logs.txt", true);
            file.WriteLine(DateTime.Now.ToString() + " " + lines);
            file.Close();
        }
    }
}
