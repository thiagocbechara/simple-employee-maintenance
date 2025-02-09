using AutoMapper;
using SimpleEmployeeMaintenance.Api.Dtos;
using SimpleEmployeeMaintenance.Domain.Entities;

namespace SimpleEmployeeMaintenance.Api.MapperProfiles;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dto => dto.FirstName, opt => opt.MapFrom(e => e.Name.First))
            .ForMember(db => db.LastName, opt => opt.MapFrom(e => e.Name.Last))
            .ForMember(db => db.Department, opt => opt.MapFrom(e => e.Department.Name))
            .ForMember(db => db.Address, opt => opt.MapFrom(e => e.Address.FullAddress))
            .ForMember(db => db.Phone, opt => opt.MapFrom(e => e.Phone.Number));
    }
}
