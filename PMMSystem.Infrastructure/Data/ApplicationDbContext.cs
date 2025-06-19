using Microsoft.EntityFrameworkCore;
using PMMSystem.Domain.Entities;
using PMMSystem.Infrastructure.Data.Configurations;

namespace PMMSystem.Infrastructure.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //  modelBuilder.ApplyConfiguration(new MaintenanceRequestConfiguration());
    //}

    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
  }
}
