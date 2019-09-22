using System.IO;
using System.Linq;
using System.Reflection;

namespace PacManLibrary
{
    class DataSource
    {
        const int n = 36;
        const int m = 29;        

        public char[,] GetMap(string path)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            const string NAME = @"PacManCore.Stuff.map.txt";
            string map;
            using (Stream stream = executingAssembly.GetManifestResourceStream(NAME))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    map = reader.ReadToEnd();
                }
            }

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
