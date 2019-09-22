using System;
using System.Collections.Generic;
using System.Text;
using PacManLibrary;
using PacManLibrary.Model;

namespace PacManCore.Model.Ghost
{
    public enum Ghost
    {
        Blinki = 1,
        Pinki,
        Inki,
        Clayd
    }

    public class GhostFactory : IGhostFactory
    {
        private const int ghostapearX = 14;
        private const int ghostapearY = 14;

        private readonly IGameContext ghostContext;

        public GhostFactory(IGameContext ghostContext)
        {
            this.ghostContext = ghostContext;
        }

        public GhostAbstract GetGhost(Ghost ghost)
        {
            switch (ghost)
            {
                case Ghost.Blinki:
                    return new BlinkiGhost(this.ghostContext) { X = ghostapearX, Y = ghostapearY };
                case Ghost.Pinki:
                    break;
                case Ghost.Inki:
                    break;
                case Ghost.Clayd:
                    break;
                default:
                    throw new ArgumentException(nameof(ghost));
            }

            return null;
        }
    }
}