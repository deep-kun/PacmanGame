using PacManLibrary.Model;

namespace PacManCore.Model.Ghost
{
    public interface IGhostFactory
    {
        GhostAbstract GetGhost(Ghost ghost);
    }
}