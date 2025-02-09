using Microsoft.Extensions.DependencyInjection;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;
using SimpleEmployeeMaintenance.Domain.MapperProfiles;

namespace SimpleEmployeeMaintenance.Domain.Extensions;

public static class DomainDependecyInjectionExtesion
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateEmployeeCommand>());

        return services;
    }
}
