using PacManLibrary.Model;

namespace PacManLibrary.Interfaces
{
    public interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
        double Xpixel { get; }
        double Ypixel { get; }
        double R { get; }
    }
}
