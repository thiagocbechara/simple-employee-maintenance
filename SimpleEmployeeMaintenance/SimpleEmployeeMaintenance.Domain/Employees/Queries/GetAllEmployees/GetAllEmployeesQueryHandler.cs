using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;

internal class GetAllEmployeesQueryHandler(IEmployeeRepository repository)
    : IRequestHandler<GetAllEmployeesQuery, Result<Pagination<Employee>>>
{
    public async Task<Result<Pagination<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<Pagination<Employee>>.ResultError("Cancellation was requested");
        }

        var results = await repository.GetPaginatedAsync(request.Page, request.QuantityPerPage);

        return Result<Pagination<Employee>>.ResultSuccess(results);
    }
}
