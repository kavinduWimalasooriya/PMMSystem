using PMMSystem.Domain.Entities;

namespace PMMSystem.Application.RepositoryInterfaces
{
  public interface IMaintenanceRequestRepository
  {
    Task<IEnumerable<MaintenanceRequest>?> GetMaintenanceRequestsAsync();
  }
}
