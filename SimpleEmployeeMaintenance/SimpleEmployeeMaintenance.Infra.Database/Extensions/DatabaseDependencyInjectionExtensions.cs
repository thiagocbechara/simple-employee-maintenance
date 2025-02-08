using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleEmployeeMaintenance.Infra.Database.DataContexts;
using SimpleEmployeeMaintenance.Infra.Database.MapperProfiles;

namespace SimpleEmployeeMaintenance.Infra.Database.Extensions;

public static class DatabaseDependencyInjectionExtensions
{
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SimpleEmployeeMaintenance")));
        services.AddAutoMapper(typeof(DatabaseProfile));

        return services;
    }
}
