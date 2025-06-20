using PMMSystem.Application.Dtos;

namespace PMMSystem.Application.Services.Interfaces
{
  public interface IMaintenanceRequestService
  {
    Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync();
    Task CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto maintenanceRequest,string? imgUrl);
    Task<MaintenanceRequestDto?> GetMaintenanceRequestByIdAsync(int id);
    Task UpdateMaintenanceRequestAsync(UpdateMaintenanceRequestDto request, string? imagePath);
  }
}
