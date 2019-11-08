using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PacaManDataAccessLayer;
using PacManCore.Model.Ghost;
using PacManLibrary;

namespace PacManWeb
{
    public static class ServiceExtensions
    {
        public static void AddPacmanDependency(this IServiceCollection services)
        {
            services.AddSingleton<IGameServies, GameServies>();
            services.AddSingleton<IHighScoreRepository, HighScoreInMemoryRepository>();
            services.AddTransient<IGame, Game>();
            services.AddSignalR(s => s.EnableDetailedErrors = true).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.WriteIndented = false;
            });
            services.AddOptions();
            services.AddTransient<GameHub>();
            services.AddTransient<IGhostFactory, GhostFactory>();
            services.AddScoped<IGameContext, GameContext>();
        }
    }
}
