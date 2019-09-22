using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class AbstractPoint : IPoint
    {
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }
        public double R { get; }

        public int X { get; set; }
        public int Y { get; set; }


        public AbstractPoint()
        {

        }
        public AbstractPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $" Actually  X={X} Y ={Y}";
        }

        public AbstractPointAproximate AproximateLocation()
        {
            return new AbstractPointAproximate(X, Y);
        }
    }
}
