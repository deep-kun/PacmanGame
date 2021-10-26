using Microsoft.Extensions.DependencyInjection;
using PacaManDataAccessLayer;
using PacManCore.Model.Ghost;
using Microsoft.AspNetCore.SignalR;
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
            services.AddSignalR(s => { s.EnableDetailedErrors = true;}).AddNewtonsoftJsonProtocol(); ;
            services.AddOptions();
            services.AddTransient<Hub, GameHub>();
            services.AddTransient<IGhostFactory, GhostFactory>();
            services.AddScoped<IGameContext, GameContext>();
        }
    }
}
