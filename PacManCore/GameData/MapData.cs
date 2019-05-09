using PacManLibrary.Interfaces;
using PacManLibrary.Model;

namespace PacManLibrary.GameData
{
    public class MapData
    {
        private const int n = 36;
        private const int m = 28;
        public IPoint[,] InitializateMap()
        {
            IPoint[,] map = new IPoint[n, m];
            int k = 0;
            var charArray = new DataSource().GetMap(@"C:\Users\deep\source\repos\PacMan\PacManLibrary\Stuff\map.txt");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (charArray[i, j] == '-')
                    {
                        map[i, j] = new EatedFood { X = i, Y = j, IsChoosable = false };
                    }
                    if (charArray[i, j] == '0')
                    {
                        map[i, j] = new Wall { X = i, Y = j };
                    }
                    if (charArray[i, j] == '*')
                    {
                        k++;
                        map[i, j] = new BaseFood { X = i, Y = j, IsChoosable = false };
                    }
                    if (charArray[i, j] == '+')
                    {
                        k++;
                        map[i, j] = new BaseFood { X = i, Y = j, IsChoosable = true };
                    }
                    if (charArray[i, j] == '#')
                    {
                        map[i, j] = new Energizer { X = i, Y = j, IsChoosable = false };
                    }
                    if (charArray[i, j] == '$')
                    {
                        map[i, j] = new Energizer { X = i, Y = j, IsChoosable = true };
                    }
                    if (charArray[i, j] == 'x')
                    {
                        map[i, j] = new EatedFood { X = i, Y = j, IsChoosable = true };
                    }
                }
            }
            return map;
        }
    }
}
