using PacManLibrary.Interfaces;

namespace PacManLibrary
{
    public static class ColisionExtension
    {
        static public bool ColisionDetecion(this IPoint based, IPoint another)
        {
            if (based.Xpixel < another.Xpixel + another.R && based.Xpixel + based.R > another.Xpixel &&
            based.Ypixel < another.Ypixel + another.R && based.Ypixel + based.R > another.Ypixel)
            {
                return true;
            }
            return false;
        }
    }
}