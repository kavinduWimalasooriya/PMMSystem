using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMMSystem.Domain.Entities;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Infrastructure.Data.Configurations
{
  public class MaintenanceRequestConfiguration : IEntityTypeConfiguration<MaintenanceRequest>
  {
    public void Configure(EntityTypeBuilder<MaintenanceRequest> builder)
    {
      builder.HasData
        (
          new MaintenanceRequest
          {
            Id = 1,
            MaintenanceEventName = "HVAC System Repair",
            PropertyName = "Oakwood Apartments - Building A",
            Description = "Air conditioning unit in lobby not cooling properly. Temperature readings show 78°F when set to 72°F. Requires immediate attention due to resident complaints.",
            Status = MaintenanceStatus.New,
            Image = null,
            Created = DateTime.Now.AddDays(-3),
            Modified = DateTime.Now.AddHours(-2)
          },

          new MaintenanceRequest
          {
            Id = 2,
            MaintenanceEventName = "Plumbing Leak Emergency",
            PropertyName = "Riverside Plaza - Unit 205",
            Description = "Water leak detected under kitchen sink. Tenant reports water damage to cabinet floor. Emergency repair needed to prevent further property damage.",
            Status = MaintenanceStatus.New,
            Image = null,
            Created = DateTime.Now.AddHours(-6),
            Modified = DateTime.Now.AddHours(-1)
          },

          new MaintenanceRequest
          {
            Id = 3,
            MaintenanceEventName = "Elevator Maintenance",
            PropertyName = "Metropolitan Heights - Tower B",
            Description = "Monthly preventive maintenance for passenger elevator. Includes safety inspection, lubrication of moving parts, and testing of emergency systems.",
            Status = MaintenanceStatus.Accepted,
            Image = null,
            Created = DateTime.Now.AddDays(-7),
            Modified = DateTime.Now.AddDays(-7)
          },

          new MaintenanceRequest
          {
            Id = 4,
            MaintenanceEventName = "Exterior Paint Touch-up",
            PropertyName = "Garden View Condominiums",
            Description = "Paint touch-up required on south-facing exterior wall. Weather damage visible on second and third floor sections. Aesthetic improvement needed before quarterly inspection.",
            Status = MaintenanceStatus.Rejected,
            Image = null,
            Created = DateTime.Now.AddDays(-14),
            Modified = DateTime.Now.AddDays(-2)
          },

          new MaintenanceRequest
          {
            Id = 5,
            MaintenanceEventName = "Security System Upgrade",
            PropertyName = "Downtown Corporate Center",
            Description = "Installation of new keycard access system for main entrance. Includes programming of master codes and integration with existing security monitoring system.",
            Status = MaintenanceStatus.Accepted,
            Image = null,
            Created = DateTime.Now.AddDays(-1),
            Modified = DateTime.Now.AddHours(-12)
          }
        );
    }
  }
}
