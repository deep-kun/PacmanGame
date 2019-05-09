using PacManLibrary;
using System;
using System.Collections.Generic;

namespace PacManWeb.Models
{
    public class ReturnData
    {
        public int Lives { get; set; }
        public float Score { get; set; }
        public int Direction { get; set; }
        public string[,] Map { get; set; }
        public Guid GameId { get; set; }
        public GameStat GameStat { get; set; }
        public List<MovebleObject> MovebleObjects;
        public int X { get; set; }
        public int Y { get; set; }
    }
}
