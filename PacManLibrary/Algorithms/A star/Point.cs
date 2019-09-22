using PacManLibrary.Interfaces;

namespace PacManLibrary.Algorithms.A_star
{
    public struct Point : IPoint
    {
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }

        public Point(int x, int y)
        {
            R = 0;
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public double R { get; set; }

        public static bool operator ==(Point obj1, Point obj2)
        {
            var b = (obj1.X == obj2.X) && (obj1.Y == obj2.Y);
            return b;
        }
        public static bool operator !=(Point obj1, Point obj2)
        {
            return !(obj1.X == obj2.X) && (obj2.Y == obj2.Y);
        }
        public override bool Equals(object obj)
        {
            var obj2 = (Point)obj;
            return (this.X == obj2.X) && (this.Y == obj2.Y);
        }
        public override int GetHashCode()
        {
            return X * Y * base.GetHashCode();
        }
    }
}
