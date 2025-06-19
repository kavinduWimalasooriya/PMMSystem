using AutoMapper;
using PMMSystem.Application.Dtos;
using PMMSystem.Domain.Entities;
using PMMSystem.Domain.Enum;

namespace PMMSystem.Application.Mappings
{
  public class MappingProfile :Profile
  {
    public MappingProfile()
    {
      CreateMap<MaintenanceRequest,MaintenanceRequestDto>();
      CreateMap<CreateMaintenanceRequestDto, MaintenanceRequest>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MaintenanceStatus.New))
               .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
               .ForMember(dest => dest.MaintenanceEventName, opt => opt.MapFrom(src => src.MaintenanceEventName))
               .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.PropertyName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
  }
}
