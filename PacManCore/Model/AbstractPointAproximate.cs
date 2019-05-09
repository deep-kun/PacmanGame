
namespace PacManLibrary.Model
{
    public class AbstractPointAproximate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public AbstractPointAproximate()
        {

        }
        public AbstractPointAproximate(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $" Actually  X={X} Y ={Y}";
        }

    }
}
