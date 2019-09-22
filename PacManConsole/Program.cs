using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PacManLibrary;
using PacManLibrary.Model;

namespace PacManConsole
{
    class Program
    {
        private Game game;
        private string[,] map;
        public Program()
        {
            game = new Game();
            //Game.GetInstance(); 
            map = InitalizeMap();
        }

        private string[,] InitalizeMap()
        {
            var tempMap = new string[game.Border.GetLength(0), game.Border.GetLength(1)];
            /*for (int i = 0; i < game.Border.GetLength(0); i++)
            {
                for (int j = 0; j < game.Border.GetLength(1); j++)
                {
                    tempMap[i, j] = game.Border[i, j].ToString();
                }
            }*/
            return tempMap;
        }

        private void KeyPressed(ConsoleKeyInfo keyinfo)
        {
            if (keyinfo.Key.ToString() == "LeftArrow")
            {
                game.MoveLeft();
            }
            if (keyinfo.Key.ToString() == "RightArrow")
            {
                game.MoveRight();
            }
            if (keyinfo.Key.ToString() == "UpArrow")
            {
                game.MoveUp();
            }
            if (keyinfo.Key.ToString() == "DownArrow")
            {
                game.MoveDown();
            }
        }

        private void CheckDiferrense()
        {
            for (int i = 0; i < game.Border.GetLength(0); i++)
            {
                for (int j = 0; j < game.Border.GetLength(1); j++)
                {
                    if (game.Border[i, j].ToString() != map[i, j])
                    {
                        Console.Write(game.Border[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        private void Draw()
        {
            for (int i = 0; i < game.Border.GetLength(0); i++)
            {
                for (int j = 0; j < game.Border.GetLength(1); j++)
                {
                    Console.Write((game.Border[i, j]).ToString()[0]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("PacMan life = {0}, Score = {1}", game.PacMan.Life, game.PacMan.Score);
        }
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(29,43);
            var program = new Program();
            Console.WriteLine("all done");
            ConsoleKeyInfo cki;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    program.KeyPressed(cki);
                };
                Console.Clear();
                program.Draw();
                Thread.Sleep(100);
            };
        }
    }
}
