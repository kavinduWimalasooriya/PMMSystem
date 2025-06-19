using PMMSystem.Domain.Enum;

namespace PMMSystem.Application.Dtos
{
  public class MaintenanceRequestDto
  {
    public int Id { get; set; }
    public string MaintenanceEventName { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public MaintenanceStatus Status { get; set; }
    public string? ImageUrl { get; set; }
  }
}
