namespace SimpleEmployeeMaintenance.Domain.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<string>> GetAllAsync();
}
