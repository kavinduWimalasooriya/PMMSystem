using Microsoft.EntityFrameworkCore;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Domain.Entities;
using PMMSystem.Infrastructure.Data;

namespace PMMSystem.Infrastructure.Repositories
{
  public class MaintenanceRequestRepository(ApplicationDbContext context) : IMaintenanceRequestRepository
  {
    public async Task CreateMaintenanceRequestAsync(MaintenanceRequest maintenanceObj)
    {
      await context.MaintenanceRequests.AddAsync(maintenanceObj);
      await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MaintenanceRequest>?> GetMaintenanceRequestsAsync()
    {
      var maintenanceRequests = await context.MaintenanceRequests.ToListAsync();
      return maintenanceRequests;
    }
  }
}
