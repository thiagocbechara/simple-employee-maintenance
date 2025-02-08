using AutoMapper;
using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Commands.UpdateEmployee;

internal class UpdateEmployeeCommandHandler(
    IEmployeeRepository repository,
    IMapper mapper)
    : IRequestHandler<UpdateEmployeeCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<Guid>.ResultError("Cancellation was requested");
        }

        var employee = mapper.Map<Employee>(request);

        if (!employee.IsValid())
        {
            return Result<Guid>.ResultError("Employee data is not valid");
        }

        await repository.UpdateAsync(employee);

        return Result<Guid>.ResultSuccess(request.Id);
    }
}
