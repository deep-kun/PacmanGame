
namespace PacManLibrary.Interfaces
{
    public interface IAlgorithm
    {
        Direction GetDirection(IPoint[,] map,IPoint from,IPoint destination);
    }
}
