using PacaManDataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PacaManDataAccessLayer
{
    public interface IHighScoreRepository
    {
        Task AddHighScore(HighScore record);
        Task<IEnumerable<HighScore>> GetTopHighScore(int limit = 10);
    }
}
