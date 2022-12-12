using GameOfLifeServer.Hubs;
using GameOfLifeServer.Helpers;
using GameOfLifeServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace GameOfLifeServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("GameOfLifeDb");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(
                    sqlConnectionString
                    ), ServiceLifetime.Singleton
                );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // autoMapper for handle services behind. 
            // Si no se especifica el tipo de servicio, se crea una nueva instancia cada vez que se solicita.
            services.AddSingleton<UserService>(); // if we want to use the same instance of the service in the entire application.
            services.AddSingleton<SessionService>(); // use same instance of service.

            services.AddSignalR(o => o.EnableDetailedErrors = true).AddJsonProtocol(
                options =>
                options.PayloadSerializerOptions.PropertyNamingPolicy = null
            );

            services.Configure<HubOptions>(options =>
            {
                options.MaximumReceiveMessageSize = null;
            }); // Esto soluciona el error de querer enviar listas demasiado grandes

            // Lines above avoid the error of trying to send too large lists.
            // Error obtained: The maximum message size of 32768B was exceeded. The message size can be configured in AddHubOptions.'
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SessionHub>("/gameoflife");
                endpoints.MapHub<GameOfLifeHub>("/gol");
            });
        }
    }
}
