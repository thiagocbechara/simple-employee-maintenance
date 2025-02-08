using SimpleEmployeeMaintenance.Domain.ValueObjects;

namespace SimpleEmployeeMaintenance.Domain.Entities;

public class Employee
{
    public Name Name { get; set; }
    public DateOnly HireDate { get; set; }
    public Department Department { get; set; } = default!;
    public Phone Phone { get; set; } = default!;
    public Address Address { get; set; } = default!;
}
