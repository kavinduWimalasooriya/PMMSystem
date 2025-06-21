using System.ComponentModel.Design;

namespace PMMSystem.Domain.Exceptions
{
  public class MaintenanceNotFoundException : Exception
  {
    public MaintenanceNotFoundException(int maintenanceRequestId)
      :base($"The maintenance request with id : {maintenanceRequestId} does not exist in the database.")
    {
      
    }
  }
}
