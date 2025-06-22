using Microsoft.AspNetCore.Mvc;
using PMMSystem.Application.Dtos;
using PMMSystem.Application.Services.Interfaces;
using PMMSystem.Domain.Enum;
using PMMSystem.Domain.Exceptions;

namespace PMMSystem.API.Controllers
{
  /// <summary>
  /// Controller for managing maintenance requests.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class MaintenanceRequestsController(IMaintenanceRequestService maintenanceRequestService, IWebHostEnvironment webHost) : ControllerBase
  {
    const string ImageFolder = "images";

    /// <summary>
    /// Retrieves a list of maintenance requests, with optional filtering by search term and status.
    /// </summary>
    /// <param name="search">Optional search term to filter requests by (e.g., event name or property name).</param>
    /// <param name="status">Optional status to filter requests by (e.g., New, Accepted, Rejected).</param>
    /// <returns>A collection of <see cref="MaintenanceRequestDto"/> objects.</returns>
    [HttpGet("maintenance-requests")]
    public async Task<ActionResult<IEnumerable<MaintenanceRequestDto>>> GetAll([FromQuery]string? search, [FromQuery] MaintenanceStatus? status) 
    { 
      var requestDtos = await maintenanceRequestService.GetMaintenanceRequestsAsync(search,status);
      return Ok(requestDtos);
    }

    /// <summary>
    /// Retrieves a specific maintenance request by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the maintenance request.</param>
    /// <returns>A <see cref="MaintenanceRequestDto"/> object.</returns>
    [HttpGet("maintenance-requests/{id:int}")]
    public async Task<ActionResult<MaintenanceRequestDto>> GetMaintenanceRequestById(int id)
    {
      var request = await maintenanceRequestService.GetMaintenanceRequestByIdAsync(id);
      return Ok(request);
    }

    /// <summary>
    /// Creates a new maintenance request.
    /// </summary>
    /// <param name="request">An instance of  <see cref="CreateMaintenanceRequestDto"/></param>
    /// <returns>An action result indicating success or failure.</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Add([FromForm] CreateMaintenanceRequestDto request)
    {
      await maintenanceRequestService.CreateMaintenanceRequestAsync(request, webHost.WebRootPath, ImageFolder);
      return Ok();
    }

    /// <summary>
    /// Updates an existing maintenance request.
    /// </summary>
    /// <param name="request">An instance of <see cref="UpdateMaintenanceRequestDto"/></param>
    /// <returns>An action result indicating success or failure.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/MaintenanceRequests
    ///     Content-Type: multipart/form-data
    ///     {
    ///        "id": 1,
    ///        "maintenanceEventName": "Leaky Faucet (Fixed)",
    ///        "propertyName": "Apartment 1A",
    ///        "description": "The kitchen faucet was repaired. No more dripping.",
    ///        "status": 2, // 0 for New , 1 for Accepted and 2 for 'Rejected' 
    ///        "role": 0, // 0 for Admin or 1 for PropertyManager 
    ///        "image": [new file, optional]
    ///     }
    ///
    /// The 'image' field expects an optional file upload. If a new image is provided, the old one will be replaced.
    /// </remarks>
    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Update([FromForm] UpdateMaintenanceRequestDto request)
    {
      await maintenanceRequestService.UpdateRequestAsync(request,webHost.WebRootPath,ImageFolder);
      return Ok();
    }
  } 
}
