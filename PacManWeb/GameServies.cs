using PacManLibrary;
using System;
using System.Collections.Generic;

namespace PacManWeb
{
    public class GameServies
    {
        public  Dictionary<Guid, Game> Games { get; set; } = new Dictionary<Guid, Game>();
    }
}
