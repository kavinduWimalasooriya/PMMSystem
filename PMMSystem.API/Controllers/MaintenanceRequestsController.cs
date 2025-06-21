using Microsoft.AspNetCore.Mvc;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Domain.Enum;
using PMMSystem.Domain.Exceptions;

namespace PMMSystem.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MaintenanceRequestsController(IMaintenanceRequestService maintenanceRequestService, IWebHostEnvironment webHost) : ControllerBase
  {
    const string ImageFolder = "images";

    [HttpGet("maintenance-requests")]
    public async Task<ActionResult<IEnumerable<MaintenanceRequestDto>>> GetAll([FromQuery]string? search, [FromQuery] MaintenanceStatus? status) 
    { 
      var requestDtos = await maintenanceRequestService.GetMaintenanceRequestsAsync(search,status);
      return Ok(requestDtos);
    }

    [HttpGet("maintenance-requests/{id:int}")]
    public async Task<ActionResult<MaintenanceRequestDto>> GetMaintenanceRequestById(int id)
    {
      var request = await maintenanceRequestService.GetMaintenanceRequestByIdAsync(id);
      return Ok(request);
    }
    [HttpPost]
    public async Task<ActionResult> Add([FromForm] CreateMaintenanceRequestDto request)
    {
      await maintenanceRequestService.CreateMaintenanceRequestAsync(request, webHost.WebRootPath, ImageFolder);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromForm] UpdateMaintenanceRequestDto request)
    {
      await maintenanceRequestService.UpdateRequestAsync(request,webHost.WebRootPath,ImageFolder);
      return Ok();
    }
  } 
}
