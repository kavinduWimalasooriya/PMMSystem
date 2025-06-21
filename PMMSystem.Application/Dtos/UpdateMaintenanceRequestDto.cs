using Microsoft.AspNetCore.Http;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Application.Dtos
{
  public class UpdateMaintenanceRequestDto
  {
    public int Id { get; set; }
    public string MaintenanceEventName { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public MaintenanceStatus Status { get; set; }
    public IFormFile? Image { get; set; }
    public UserRole Role { get; set; }
  }
}
