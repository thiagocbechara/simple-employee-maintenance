namespace SimpleEmployeeMaintenance.Infra.Database.Entities;

internal class DepartmentDb
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public IEnumerable<EmployeeDb> Employees { get; set; } = default!;
}
