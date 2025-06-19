using Microsoft.AspNetCore.Http;

namespace PMMSystem.Application.Dtos
{
  public class CreateMaintenanceRequestDto
  {
    public string MaintenanceEventName { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
  }
}
