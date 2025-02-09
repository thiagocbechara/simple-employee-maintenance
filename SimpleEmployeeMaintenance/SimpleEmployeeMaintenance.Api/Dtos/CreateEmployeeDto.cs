namespace SimpleEmployeeMaintenance.Api.Dtos;

public class CreateEmployeeDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Department { get; set; } = default!;
    public DateOnly HireDate { get; set; }
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;
}
