using PacManLibrary;
using System;
using System.Collections.Generic;

namespace PacManWeb
{
    public class GameServies : IGameServies
    {
        public Dictionary<Guid, IGame> Games { get; private set; } = new Dictionary<Guid, IGame>();
    }
}
