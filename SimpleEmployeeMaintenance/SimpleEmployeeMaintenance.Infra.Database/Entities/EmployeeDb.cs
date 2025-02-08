namespace SimpleEmployeeMaintenance.Infra.Database.Entities;

internal class EmployeeDb
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly HireDate { get; set; }
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;

    public DepartmentDb Department { get; set; } = default!;
}
