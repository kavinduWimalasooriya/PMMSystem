using Microsoft.EntityFrameworkCore;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Domain.Entities;
using PMMSystem.Domain.Enum;
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

    public async Task<MaintenanceRequest?> GetMaintenanceRequestByIdAsync(int id)
    {
      return await context.MaintenanceRequests.FindAsync(id);
    }

    public async Task<IEnumerable<MaintenanceRequest>?> GetMaintenanceRequestsAsync(string? search, MaintenanceStatus? status)
    {
      IQueryable<MaintenanceRequest> query = context.MaintenanceRequests;
      if (search != null) 
      {
        query = query.Where(m => m.MaintenanceEventName.Contains(search) || m.PropertyName.Contains(search));
      }
      if (status.HasValue)
      {
        query = query.Where(m => m.Status == status.Value);
      }

      query = query.OrderByDescending(r => r.Created);

      return await query.ToListAsync();
    }

    public async Task UpdateMaintenanceRequestAsync(MaintenanceRequest newObj)
    {
      context.MaintenanceRequests.Update(newObj);
      await context.SaveChangesAsync();
    }
  }
}
