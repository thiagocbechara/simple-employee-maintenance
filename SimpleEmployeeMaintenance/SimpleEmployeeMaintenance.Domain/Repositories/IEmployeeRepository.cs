using SimpleEmployeeMaintenance.Domain.Entities;

namespace SimpleEmployeeMaintenance.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Guid> SaveAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task<int> DeleteAsync(Guid id);
    Task<Employee?> GetByIdAsync(Guid id);
}
