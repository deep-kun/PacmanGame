using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class Wall : IWall
    {
        public double R { get; set; } = 0;

        public int X { get; set; }
        public int Y { get; set; }
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }

        public override string ToString()
        {
            return "█";
        }

    }
}
