using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class BaseFood : IEtable, IChoosable
    {
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }
        public double R { get; } = 0.5;
        public int X { get; set; }
        public int Y { get; set; }
        public float ConsumptionValue { get; } = 5;
        public bool IsChoosable { get; set; }
        public override string ToString()
        {
            return ".";
        }
    }
}
