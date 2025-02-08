using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeesPaginated;

public class GetEmployeesPaginatedQuery : IRequest<Result<Pagination<Employee>>>
{
    public int Page { get; set; }
    public int QuantityPerPage { get; set; }
}
