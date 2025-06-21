using PMMSystem.Application.Dtos;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Application.Services.Interfaces
{
  public interface IMaintenanceRequestService
  {
    Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync(string? search, MaintenanceStatus? staus);
    Task CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto maintenanceRequest, string webRootPath, string imageFolder);
    Task<MaintenanceRequestDto?> GetMaintenanceRequestByIdAsync(int id);
    Task UpdateRequestAsync(UpdateMaintenanceRequestDto request, string webRootPath, string imageFolder);
  }
}
