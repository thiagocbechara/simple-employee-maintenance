using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeMaintenance.Domain.Departments.Queries;

namespace SimpleEmployeeMaintenance.Api.Controllers;

/// <summary>
/// Routes for departments
/// </summary>
/// <param name="mediator"></param>
/// <param name="logger"></param>
[Route("api/department")]
[ApiController]
public class DepartmentController(
    ISender mediator,
    ILogger<EmployeeController> logger)
    : ControllerBase
{
    /// <summary>
    /// Get all departments
    /// </summary>
    /// <response code="200">List of all departments</response>
    /// <response code="404">Error message when could't get all departments</response>
    /// <response code="500">Unexpected error message</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
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
