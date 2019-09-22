using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PacManDataLayer.Entities
{
    public class PacManDbContext : DbContext
    {
        public DbSet<HighScore> HighScores { get; set; }
        public PacManDbContext(DbContextOptions<PacManDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
