using System;
using System.Collections.Generic;
using System.Text;
using PacManLibrary;
using PacManLibrary.Model;

namespace PacManCore.Model.Ghost
{
    public enum Ghost
    {
        Blinki,
        Pinki,
        Inki,
        Clayd
    }

    public class GhostFactory : IGhostFactory
    {
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
                    return new BlinkiGhost(this.ghostContext);
                case Ghost.Pinki:
                    return new PinkiGhost(this.ghostContext);
                case Ghost.Inki:
                    return new InkiGhost(this.ghostContext);
                case Ghost.Clayd:
                    return new ClaydGhost(this.ghostContext);
                default:
                    throw new ArgumentException(nameof(ghost));
            }

            return null;
        }
    }
}