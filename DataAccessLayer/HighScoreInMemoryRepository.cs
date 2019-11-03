using PacaManDataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaManDataAccessLayer
{
    public class HighScoreInMemoryRepository : IHighScoreRepository
    {
        readonly List<HighScore> inMemoryCollection = new List<HighScore>();
        public Task AddHighScore(HighScore record)
        {
            inMemoryCollection.Add(record);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<HighScore>> GetTopHighScore(int limit = 10)
        {
            return Task.FromResult(inMemoryCollection.Take(limit));
        }
    }
}
