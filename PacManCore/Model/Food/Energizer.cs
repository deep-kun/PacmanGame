using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class Energizer : IEtable, IChoosable, IBehaviorChangeble
    {
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }
        public double R { get; } = 0.8;
        public int X { get; set; }
        public int Y { get; set; }
        public float ConsumptionValue { get; } = 10;
        public bool IsChoosable { get ; set ; }

        public override string ToString()
        {
            return "#";
        }
    }
}
