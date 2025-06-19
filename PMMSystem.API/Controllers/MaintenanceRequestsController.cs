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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaintenanceRequestDto>>> GetAll() 
    { 
      var requestDtos = await maintenanceRequestService.GetMaintenanceRequestsAsync();
      return Ok(requestDtos);
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
  } 
}
