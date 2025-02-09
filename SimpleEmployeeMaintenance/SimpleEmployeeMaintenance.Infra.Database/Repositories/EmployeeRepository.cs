using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;
using SimpleEmployeeMaintenance.Infra.Database.DataContexts;
using SimpleEmployeeMaintenance.Infra.Database.Entities;

namespace SimpleEmployeeMaintenance.Infra.Database.Repositories;

internal class EmployeeRepository(
    AppDataContext context,
    IMapper mapper)
    : IEmployeeRepository
{
    public Task<int> DeleteAsync(Guid id)
    {
        var deletedCount = context.Employees
                            .Where(e => e.Id == id)
                            .ExecuteDeleteAsync();

        return deletedCount;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .ProjectTo<Employee>(mapper.ConfigurationProvider)
                .ToArrayAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        var employeeDb = await context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        var employee = mapper.Map<Employee>(employeeDb);
        return employee;
    }

    public async Task<Pagination<Employee>> GetPaginatedAsync(int page, int quantityPerPage)
    {
        var baseQuery = context.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .AsQueryable();

        var count = await baseQuery.CountAsync();
        var results = await baseQuery
            .Skip((page - 1) * quantityPerPage)
            .Take(quantityPerPage)
            .ProjectTo<Employee>(mapper.ConfigurationProvider)
            .ToArrayAsync();

        return new Pagination<Employee>
        {
            CurrentPage = page,
            ResultsPerPage = quantityPerPage,
            TotalPages = count / quantityPerPage,
            TotalResults = count,
            Results = results
        };
    }

    public async Task<Guid> SaveAsync(Employee employee)
    {
        var employeeDb = mapper.Map<EmployeeDb>(employee);

        var department = await context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Name == employee.Department.Name);
        if(department is not null)
        {
            employeeDb.DepartmentId = department.Id;
            employeeDb.Department = null;
        }

        var entry = context.Employees.Add(employeeDb);

        await context.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task UpdateAsync(Employee employee)
    {
        var employeeDb = mapper.Map<EmployeeDb>(employee);

        context.Employees.Update(employeeDb);
        await context.SaveChangesAsync();
    }
}
