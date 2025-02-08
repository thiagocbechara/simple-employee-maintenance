using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<Result<Pagination<Employee>>>
{
    public int Page { get; set; }
    public int QuantityPerPage { get; set; }
}
