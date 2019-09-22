using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class EatedFood : IEtable,IChoosable
    {
        public double R { get; } = 0.5;
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }
        public bool IsChoosable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float ConsumptionValue { get; } = 0;

        public override string ToString()
        {
            return " ";
        }
    }
}
