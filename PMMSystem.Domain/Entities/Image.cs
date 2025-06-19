namespace PMMSystem.Domain.Entities
{
  public class Image : BaseEntity
  {
    public required string ImageName { get; set; } 
    public required string ImageOriginalName { get; set; }
    public required string FilePath { get; set; }
    public int MaintenanceRequestId { get; set; }
    public MaintenanceRequest MaintenanceRequest { get; set; } = null!;

  }
}