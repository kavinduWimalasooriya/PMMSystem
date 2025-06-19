using Microsoft.EntityFrameworkCore;
using PMMSystem.Domain.Entities;

namespace PMMSystem.Infrastructure.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
  }
}
