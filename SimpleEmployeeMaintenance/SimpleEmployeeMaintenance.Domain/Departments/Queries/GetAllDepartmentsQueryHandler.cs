using MediatR;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Departments.Queries;

internal class GetAllDepartmentsQueryHandler(IDepartmentRepository repository)
    : IRequestHandler<GetAllDepartmentsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Result<IEnumerable<string>>.ResultError("Cancellation was requested");
        }

        var results = await repository.GetAllAsync();

        return Result<IEnumerable<string>>.ResultSuccess(results);
    }
}
