using PMMSystem.Domain.Entities;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Application.RepositoryInterfaces
{
  public interface IMaintenanceRequestRepository
  {
    Task CreateMaintenanceRequestAsync(MaintenanceRequest maintenanceObj);
    Task<MaintenanceRequest?> GetMaintenanceRequestByIdAsync(int id);
    Task<IEnumerable<MaintenanceRequest>?> GetMaintenanceRequestsAsync(string? search,MaintenanceStatus? status);
    Task UpdateMaintenanceRequestAsync(MaintenanceRequest newObj);
  }
}
