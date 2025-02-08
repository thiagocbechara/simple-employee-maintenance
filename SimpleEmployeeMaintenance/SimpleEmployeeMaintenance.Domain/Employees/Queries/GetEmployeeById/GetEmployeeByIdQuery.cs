using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<Result<Employee?>>
{
    public Guid Id { get; set; }
}
