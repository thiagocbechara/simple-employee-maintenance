using MediatR;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Employees.Commands.DeleteEmployee;

internal class DeleteEmployeeCommandHandler(IEmployeeRepository repository)
    : IRequestHandler<DeleteEmployeeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<int>.ResultError("Cancellation was requested");
        }

        var delete = await repository.DeleteAsync(request.Id);

        return Result<int>.ResultSuccess(delete);
    }
}
