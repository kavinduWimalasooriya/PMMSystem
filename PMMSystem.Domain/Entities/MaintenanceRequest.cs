using PMMSystem.Domain.Enum;

namespace PMMSystem.Domain.Entities
{
  public class MaintenanceRequest :BaseEntity
  {
    public required string MaintenanceEventName { get; set; }
    public required string PropertyName { get; set; }
    public required string Description { get; set; }
    public MaintenanceStatus Status { get; set; }
    public string? ImageUrl { get; set; }
  }
}
