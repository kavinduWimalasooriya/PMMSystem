﻿using PMMSystem.Application;
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
        builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
      });
    }

    public static void ConfigureLogger(this IServiceCollection services) 
    {
      services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureServices(this IServiceCollection services) 
    {
      services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();
      services.AddScoped<IFileService, FileService>();
    }

    public static void ConfigureRepositories( this IServiceCollection services) 
    {
      services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
    }
  }
}
