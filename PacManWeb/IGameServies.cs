using System;
using System.Collections.Generic;
using PacManLibrary;

namespace PacManWeb
{
    public interface IGameServies
    {
        Dictionary<Guid, Game> Games { get; }
    }
}