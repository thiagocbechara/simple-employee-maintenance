using Microsoft.EntityFrameworkCore;
using SimpleEmployeeMaintenance.Domain.Repositories;
using SimpleEmployeeMaintenance.Infra.Database.DataContexts;

namespace SimpleEmployeeMaintenance.Infra.Database.Repositories;

internal class DepartmentRepository(AppDataContext context)
    : IDepartmentRepository
{
    public async Task<IEnumerable<string>> GetAllAsync()
    {
        var allDepartments = await context.Departments
                                    .Select(x => x.Name)
                                    .OrderByDescending(x => x)
                                    .ToArrayAsync();
        return allDepartments;
    }
}
