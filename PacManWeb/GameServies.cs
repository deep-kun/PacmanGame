using PacManLibrary;
using System;
using System.Collections.Generic;

namespace PacManWeb
{
    public class GameServies : IGameServies
    {
        public Dictionary<Guid, Game> Games { get; private set; } = new Dictionary<Guid, Game>();
    }
}
