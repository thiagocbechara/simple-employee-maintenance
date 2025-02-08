using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeeById;

internal class GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    : IRequestHandler<GetEmployeeByIdQuery, Result<Employee?>>
{
    public async Task<Result<Employee?>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<Employee?>.ResultError("Cancellation was requested");
        }

        var employee = await repository.GetByIdAsync(request.Id);

        return employee is null
            ? Result<Employee?>.ResultError("Employee not found")
            : Result<Employee?>.ResultSuccess(employee);
    }
}
