using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Guid> SaveAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task<int> DeleteAsync(Guid id);
    Task<Employee?> GetByIdAsync(Guid id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Pagination<Employee>> GetPaginatedAsync(int page, int quantityPerPage);
}
