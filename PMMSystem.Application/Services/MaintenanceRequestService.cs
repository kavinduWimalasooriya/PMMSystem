using AutoMapper;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Domain.Entities;

namespace PMMSystem.Application.Services
{
  public class MaintenanceRequestService(IMaintenanceRequestRepository maintenanceRepo,IMapper mapper) : IMaintenanceRequestService
  {
    public async Task CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto maintenanceRequest,string? imgUrl)
    {
      var maintenanceObj = mapper.Map<MaintenanceRequest>(maintenanceRequest);
      maintenanceObj.ImageUrl = imgUrl;
      await maintenanceRepo.CreateMaintenanceRequestAsync(maintenanceObj);
    }

    public async Task<MaintenanceRequestDto?> GetMaintenanceRequestByIdAsync(int id)
    {
      var maintenanceObj = await maintenanceRepo.GetMaintenanceRequestByIdAsync(id);
      var maintenanceReq = mapper.Map<MaintenanceRequestDto>(maintenanceObj);
      return maintenanceReq;
    }

    public async Task<IEnumerable<MaintenanceRequestDto>?> GetMaintenanceRequestsAsync()
    {
      var requestObj = await maintenanceRepo.GetMaintenanceRequestsAsync();
      var requestDtos = mapper.Map<IEnumerable<MaintenanceRequestDto>>(requestObj);
      return requestDtos;
    }
  }
}
