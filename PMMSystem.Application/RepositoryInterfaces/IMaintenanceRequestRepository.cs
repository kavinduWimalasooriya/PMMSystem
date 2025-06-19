using PMMSystem.Domain.Entities;

namespace PMMSystem.Application.RepositoryInterfaces
{
  public interface IMaintenanceRequestRepository
  {
    Task CreateMaintenanceRequestAsync(MaintenanceRequest maintenanceObj);
    Task<MaintenanceRequest?> GetMaintenanceRequestByIdAsync(int id);
    Task<IEnumerable<MaintenanceRequest>?> GetMaintenanceRequestsAsync();
  }
}
