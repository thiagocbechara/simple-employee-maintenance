using MediatR;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Domain.Departments.Queries;

public class GetAllDepartmentsQuery : IRequest<Result<IEnumerable<string>>>
{
}
