using PMMSystem.Application;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Application.Services;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Infrastructure;
using PMMSystem.Infrastructure.Repositories;

namespace PMMSystem.API.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureCors(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("https://loaclhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());
      });
    }

    public static void ConfigureLogger(this IServiceCollection services) 
    {
      services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureServices(this IServiceCollection services) 
    {
      services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();
    }

    public static void ConfigureRepositories( this IServiceCollection services) 
    {
      services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
    }
  }
}
