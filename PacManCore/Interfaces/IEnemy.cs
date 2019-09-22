
namespace PacManLibrary.Interfaces
{
    public interface IEnemy : IMoveble, IEtable
    {
        Direction Direction { get;}
        IPoint OnPoint { get; set; }
        bool Hiden { get; set; }
    }
}
