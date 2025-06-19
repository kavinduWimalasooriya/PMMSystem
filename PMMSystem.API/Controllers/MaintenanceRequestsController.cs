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

    [HttpGet("maintenance-requets/{id:int}")]
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
  } 
}
