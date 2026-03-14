using AssetTrack.API.Models;
using AssetTrack.Application.DTOs;
using AutoMapper;

namespace AssetTrack.Application.Mappings;

public class AssetProfile : Profile
{
    public AssetProfile()
    {
        // Source -> Destination
        
        // When sending data to the Frontend
        CreateMap<Asset, AssetDto>()
            .ForMember(dest => dest.AssignedEmployeeName, 
                opt => opt.MapFrom(src => src.AssignedEmployee != null 
                    ? $"{src.AssignedEmployee.FirstName} {src.AssignedEmployee.LastName}" 
                    : "In Storage"));

        // When receiving data from the Frontend
        CreateMap<AssetDto, Asset>();
    }
}