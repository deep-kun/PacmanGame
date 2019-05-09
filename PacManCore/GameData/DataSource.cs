using System.IO;

namespace PacManLibrary
{
    class DataSource
    {
        const int n = 36;
        const int m = 29;        

        public char[,] GetMap(string path)
        {
            string map = File.ReadAllText(path);
            char[,] charArray = new char[n, m];
            var lines = map.Split(new[] { '\n' });
            int row = 0;
            foreach (string line in lines)
            {
                int column = 0;
                foreach (char character in line)
                {
                    charArray[row, column] = character;
                    column++;
                }
                row++;
            }
            return charArray;
        }


    }
}
