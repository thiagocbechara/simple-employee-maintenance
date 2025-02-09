using AutoMapper;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.ValueObjects;
using SimpleEmployeeMaintenance.Infra.Database.Entities;

namespace SimpleEmployeeMaintenance.Infra.Database.MapperProfiles;

internal class DatabaseProfile : Profile
{
    public DatabaseProfile()
    {
        CreateMap<Employee, EmployeeDb>()
            .ForMember(db => db.FirstName, opt => opt.MapFrom(e => e.Name.First))
            .ForMember(db => db.LastName, opt => opt.MapFrom(e => e.Name.Last))
            .ForMember(db => db.Department, opt => opt.MapFrom(e => new DepartmentDb { Name = e.Department }))
            .ForMember(db => db.Address, opt => opt.MapFrom(e => e.Address.FullAddress))
            .ForMember(db => db.Phone, opt => opt.MapFrom(e => e.Phone.Number))
            .ReverseMap()
            .ForMember(e => e.Name, opt => opt.MapFrom(db => new Name(db.FirstName, db.LastName)))
            .ForMember(e => e.Department, opt => opt.MapFrom(db => db.Department!.Name));
    }
}
