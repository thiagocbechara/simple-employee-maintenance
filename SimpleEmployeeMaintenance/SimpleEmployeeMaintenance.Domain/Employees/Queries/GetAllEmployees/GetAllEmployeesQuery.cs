using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<Result<IEnumerable<Employee>>>
{
}
