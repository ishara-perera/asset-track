using AssetTrack.API.Models;
using AssetTrack.Application.DTOs;
using AssetTrack.Domain.Entities;
using AutoMapper;

namespace AssetTrack.API.Services;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        // Entity -> DTO (Used for GET requests)
        CreateMap<Employee, EmployeeDto>()
            // Map the calculated FullName property
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            
            // Map the Count of assigned assets to avoid circular references
            .ForMember(dest => dest.AssignedAssetCount, 
                opt => opt.MapFrom(src => src.Assets != null ? src.Assets.Count : 0));

        // DTO -> Entity (Used for POST/PUT requests)
        CreateMap<EmployeeDto, Employee>()
            // We ignore the ID during creation as the database handles it
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            // We ignore the Assets collection when creating an employee from a DTO
            .ForMember(dest => dest.Assets, opt => opt.Ignore());
    }
}