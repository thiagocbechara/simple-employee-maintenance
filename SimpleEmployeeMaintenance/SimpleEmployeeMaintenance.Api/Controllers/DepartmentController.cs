using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeMaintenance.Domain.Departments.Queries;

namespace SimpleEmployeeMaintenance.Api.Controllers;

[Route("api/department")]
[ApiController]
public class DepartmentController(
    ISender mediator,
    ILogger<EmployeeController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var result = await mediator.Send(new GetAllDepartmentsQuery());

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetAllAsync));
            return new ObjectResult("An error has occurred")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
