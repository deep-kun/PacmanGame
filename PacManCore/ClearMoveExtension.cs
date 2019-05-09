using PacManLibrary.Interfaces;

namespace PacManLibrary
{
    public static class ClearMoveExtension
    {
        public static bool AvalaibleY(this IPoint p)
        {
            if (p.Ypixel % p.Y == 0)
            {
                return true;
            }
            return false;
        }
        public static bool AvalaibleX(this IPoint p)
        {
            if (p.Xpixel % p.X == 0)
            {
                return true;
            }
            return false;
        }
    }
}
