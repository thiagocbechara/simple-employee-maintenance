using AutoMapper;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.UpdateEmployee;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.ValueObjects;

namespace SimpleEmployeeMaintenance.Domain.MapperProfiles;

internal class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(e => e.Name, opt => opt.MapFrom(c => new Name(c.FirstName, c.LastName)));

        CreateMap<UpdateEmployeeCommand, Employee>()
            .ForMember(e => e.Name, opt => opt.MapFrom(c => new Name(c.FirstName, c.LastName)));
    }
}
