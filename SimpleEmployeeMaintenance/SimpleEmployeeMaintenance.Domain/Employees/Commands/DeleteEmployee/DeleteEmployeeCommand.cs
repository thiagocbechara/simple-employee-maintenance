using MediatR;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Result<int>>
{
    public Guid Id { get; set; }
}
