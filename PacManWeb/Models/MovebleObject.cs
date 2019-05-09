using PacManLibrary.Interfaces;

namespace PacManWeb.Models
{
    public class MovebleObject
    {
        public string Name { get; set; }
        public double Xp { get; set; }
        public double Yp { get; set; }
        public MovebleObject(IPoint point)
        {
            Name = point.ToString();
            Xp = point.Xpixel;
            Yp = point.Ypixel;
        }
    }
}
