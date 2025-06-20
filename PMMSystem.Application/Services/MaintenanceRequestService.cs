using AutoMapper;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.RepositoryInterfaces;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Domain.Entities;

namespace PMMSystem.Application.Services
{
  public class MaintenanceRequestService(IMaintenanceRequestRepository maintenanceRepo,IMapper mapper,IFileService fileService) : IMaintenanceRequestService
  {
    public async Task CreateMaintenanceRequestAsync(CreateMaintenanceRequestDto maintenanceRequest, string webRootPath, string imageFolder)
    {
      var maintenanceObj = mapper.Map<MaintenanceRequest>(maintenanceRequest);
      string? imagePath = null;
      if (maintenanceRequest.Image != null)
      {
        imagePath = await fileService.SaveFileAsync(maintenanceRequest.Image, webRootPath, imageFolder);
      }
      maintenanceObj.ImageUrl = imagePath;
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

    public Task UpdateMaintenanceRequestAsync(UpdateMaintenanceRequestDto request, string? imagePath)
    {
      var requestObj = mapper.Map<MaintenanceRequest>(request);
      throw new NotImplementedException();
    }

    public async Task UpdateRequestAsync(UpdateMaintenanceRequestDto request, string webRootPath, string imageFolder)
    {
      var existingObj = await maintenanceRepo.GetMaintenanceRequestByIdAsync(request.Id);
      if(existingObj == null)
        throw new NotImplementedException();
      mapper.Map(request, existingObj);

      existingObj.Modified = DateTime.UtcNow;
      string? imagePath = existingObj.ImageUrl;

      if (request.Image != null)
      {
        if (!string.IsNullOrEmpty(imagePath))
        {
          var imgFilePath = Path.Combine(webRootPath, imageFolder, imagePath);
          await fileService.DeleteFileAsync(imgFilePath);
        }
        imagePath = await fileService.SaveFileAsync(request.Image, webRootPath, imageFolder);
      }
      existingObj.ImageUrl = imagePath;
      await maintenanceRepo.UpdateMaintenanceRequestAsync(existingObj);
    }
  }
}
