using PacManLibrary.Interfaces;

namespace PacManLibrary.Model
{
    public class CherryFood : IEtable, IDisappearable
    {       
        private const int itetatinToDisapper = 650;
        private readonly IPoint[,] map;

        public CherryFood(IGameContext context)
        {
            this.map = context.Map;
        }

        public double R { get; } = 0.7;
        public double Xpixel { get => X; }
        public double Ypixel { get => Y; }
        private int attemp = 0;
        public int X { get; set; }
        public int Y { get; set; }
        public float ConsumptionValue { get; } = 100;
        public bool IsChoosable { get; set; }

        public void Disapper()
        {
            if (attemp < itetatinToDisapper)
            {
                attemp++;
                return;
            }
            if (map[X, Y] is IEnemy enemy)
            {
                enemy.OnPoint = new EatedFood();
                return;
            }
            map[X, Y] = new EatedFood();
        }

        public override string ToString()
        {
            return GetType().Name;
        }
        
    }
}
