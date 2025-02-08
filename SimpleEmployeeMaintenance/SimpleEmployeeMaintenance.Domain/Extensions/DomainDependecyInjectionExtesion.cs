using Microsoft.Extensions.DependencyInjection;
using SimpleEmployeeMaintenance.Domain.MapperProfiles;

namespace SimpleEmployeeMaintenance.Domain.Extensions;

public static class DomainDependecyInjectionExtesion
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainProfile));

        return services;
    }
}
