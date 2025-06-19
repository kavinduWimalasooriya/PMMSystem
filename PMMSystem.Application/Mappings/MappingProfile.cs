using AutoMapper;
using PMMSystem.Application.Dtos;
using PMMSystem.Domain.Entities;

namespace PMMSystem.Application.Mappings
{
  public class MappingProfile :Profile
  {
    public MappingProfile()
    {
      CreateMap<MaintenanceRequest,MaintenanceRequestDto>();
    }
  }
}
