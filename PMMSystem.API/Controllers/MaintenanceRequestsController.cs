using Microsoft.AspNetCore.Mvc;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.Services.Interfaces;

namespace PMMSystem.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MaintenanceRequestsController(IMaintenanceRequestService maintenanceRequestService, IWebHostEnvironment webHost,IFileService fileService) : ControllerBase
  {
    const string ImageFolder = "images";

    [HttpGet("maintenance-requests")]
    public async Task<ActionResult<IEnumerable<MaintenanceRequestDto>>> GetAll() 
    { 
      var requestDtos = await maintenanceRequestService.GetMaintenanceRequestsAsync();
      return Ok(requestDtos);
    }

    [HttpGet("maintenance-requests/{id:int}")]
    public async Task<ActionResult<MaintenanceRequestDto>> GetMaintenanceRequestById(int id)
    {
      var request = await maintenanceRequestService.GetMaintenanceRequestByIdAsync(id);
      if(request == null)
        return NotFound();
      return Ok(request);
    }
    [HttpPost]
    public async Task<ActionResult> Add(CreateMaintenanceRequestDto request)
    {
      string? imagePath = null;
      if (request.Image != null)
      {
        imagePath = await fileService.SaveFileAsync(request.Image,webHost.WebRootPath,ImageFolder);
      }

      await maintenanceRequestService.CreateMaintenanceRequestAsync(request, imagePath);

      return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromForm] UpdateMaintenanceRequestDto request)
    {
      var existingRequest = await maintenanceRequestService.GetMaintenanceRequestByIdAsync(request.Id);
      if (existingRequest == null)
      {
        return NotFound($"Maintenance Request with ID {request.Id} not found.");
      }

      string? imagePath = existingRequest.ImageUrl;

      if (request.Image != null)
      {
        if (!string.IsNullOrEmpty(imagePath))
        {
          var imgFilePath = Path.Combine(webHost.WebRootPath,ImageFolder, imagePath);
          await fileService.DeleteFileAsync(imgFilePath);
        }
        imagePath = await fileService.SaveFileAsync(request.Image, webHost.WebRootPath, ImageFolder);
      }

      await maintenanceRequestService.UpdateMaintenanceRequestAsync(request, imagePath);

      return Ok();
    }
  } 
}
