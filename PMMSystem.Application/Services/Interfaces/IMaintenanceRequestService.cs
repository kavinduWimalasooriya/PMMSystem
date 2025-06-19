using PMMSystem.Application.Dtos;

namespace PMMSystem.Application.Services.Interfaces
{
  public interface IMaintenanceRequestService
  {
    Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync();
  }
}
