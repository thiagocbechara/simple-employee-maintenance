using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;

internal class GetAllEmployeesQueryHandler(IEmployeeRepository repository)
    : IRequestHandler<GetAllEmployeesQuery, Result<IEnumerable<Employee>>>
{
    public async Task<Result<IEnumerable<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<IEnumerable<Employee>>.ResultError("Cancellation was requested");
        }

        var allEmployees = await repository.GetAllAsync();

        return Result<IEnumerable<Employee>>.ResultSuccess(allEmployees);
    }
}
