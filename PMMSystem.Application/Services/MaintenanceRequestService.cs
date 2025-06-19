using AutoMapper;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Application.Services.Interfaces;

namespace PMMSystem.Application.Services
{
  public class MaintenanceRequestService(IMaintenanceRequestRepository maintenanceRepo,IMapper mapper) : IMaintenanceRequestService
  {
    public async Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync()
    {
      var requestObj = await maintenanceRepo.GetMaintenanceRequestsAsync();
      var requestDtos = mapper.Map<IEnumerable<MaintenanceRequestDto>>(requestObj);
      return requestDtos;
    }
  }
}
