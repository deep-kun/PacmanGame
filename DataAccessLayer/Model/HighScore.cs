using System;
using System.Collections.Generic;
using System.Text;

namespace PacaManDataAccessLayer.Model
{
    public class HighScore
    {
        public float Score { get; set; }
        public string Name { get; set; }
        public Guid GameId { get; set; }
        public DateTime Date { get; set; }
    }
}
