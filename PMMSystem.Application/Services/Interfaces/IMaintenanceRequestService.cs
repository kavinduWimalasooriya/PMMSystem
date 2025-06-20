using PMMSystem.Application.Dtos;

namespace PMMSystem.Application.Services.Interfaces
{
  public interface IMaintenanceRequestService
  {
    Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync();
    Task CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto maintenanceRequest, string webRootPath, string imageFolder);
    Task<MaintenanceRequestDto?> GetMaintenanceRequestByIdAsync(int id);
    Task UpdateMaintenanceRequestAsync(UpdateMaintenanceRequestDto request, string? imagePath);
    Task UpdateRequestAsync(UpdateMaintenanceRequestDto request, string webRootPath, string imageFolder);
  }
}
