using Microsoft.EntityFrameworkCore;
using NLog;
using PMMSystem.API.Extensions;
using PMMSystem.Application;
using PMMSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureLogger();
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddAutoMapper(typeof(ServiceAssembleRefference));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
