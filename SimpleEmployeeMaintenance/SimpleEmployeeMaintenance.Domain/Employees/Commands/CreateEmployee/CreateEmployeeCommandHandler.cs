using AutoMapper;
using MediatR;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;

internal class CreateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IMapper mapper)
    : IRequestHandler<CreateEmployeeCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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
        
        var id = await employeeRepository.SaveAsync(employee);

        return Result<Guid>.ResultSuccess(id);
    }
}
