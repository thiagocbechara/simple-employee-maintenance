using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeMaintenance.Api.Dtos;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.DeleteEmployee;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeeById;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeesPaginated;
using SimpleEmployeeMaintenance.Domain.Models;

namespace SimpleEmployeeMaintenance.Api.Controllers;

/// <summary>
/// Routes for employees
/// </summary>
/// <param name="mediator"></param>
/// <param name="logger"></param>
[Route("api/employee/")]
[ApiController]
public class EmployeeController(
    ISender mediator,
    ILogger<EmployeeController> logger)
    : ControllerBase
{
    /// <summary>
    /// Create a new employee
    /// </summary>
    /// <param name="createEmployeeDto">Employee data</param>
    /// <response code="200">Id for new employee</response>
    /// <response code="400">Employee data has errors</response>
    /// <response code="500">Unexpected error message</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> PostAsync([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        try
        {
            if (createEmployeeDto is null)
            {
                return BadRequest("Employee cannot be null");
            }

            var command = new CreateEmployeeCommand
            {
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                Department = createEmployeeDto.Department,
                HireDate = createEmployeeDto.HireDate,
                Phone = createEmployeeDto.Phone,
                Address = createEmployeeDto.Address,
            };
            var result = await mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.ErrorMesage);

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(PostAsync));
            return InternalError();
        }
    }

    /// <summary>
    /// Get a employee by Id
    /// </summary>
    /// <param name="id">Employee's Id</param>
    /// <param name="mapper"></param>
    /// <response code="200">Employee data for informed Id</response>
    /// <response code="404">Employee not found for informed Id</response>
    /// <response code="500">Unexpected error message</response>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        [FromServices] IMapper mapper)
    {
        try
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var result = await mediator.Send(query);

            return result.IsSuccess
                ? Ok(mapper.Map<EmployeeDto>(result.Value))
                : NotFound(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetAsync));
            return InternalError();
        }
    }

    /// <summary>
    /// Delete a employee by Id
    /// </summary>
    /// <param name="id">Employee's Id</param>
    /// <response code="204">Employee was deleted</response>
    /// <response code="400">Error message when could't delete employee</response>
    /// <response code="500">Unexpected error message</response>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            var query = new DeleteEmployeeCommand { Id = id };
            var result = await mediator.Send(query);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(DeleteAsync));
            return InternalError();
        }
    }

    /// <summary>
    /// Get all employees
    /// </summary>
    /// <param name="mapper"></param>
    /// <response code="200">List of all employees</response>
    /// <response code="404">Error message when could't get all employees</response>
    /// <response code="500">Unexpected error message</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> GetAllAsync([FromServices] IMapper mapper)
    {
        try
        {
            var result = await mediator.Send(new GetAllEmployeesQuery());

            return result.IsSuccess
                ? Ok(mapper.Map<IEnumerable<EmployeeDto>>(result.Value))
                : NotFound(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetAllAsync));
            return InternalError();
        }
    }

    /// <summary>
    /// Get employees paginated
    /// </summary>
    /// <param name="page">Desired page from pagination</param>
    /// <param name="pageSize">Pages size</param>
    /// <param name="mapper"></param>
    /// <response code="200">List of all employees in the current page</response>
    /// <response code="404">Error message when could't get employees</response>
    /// <response code="500">Unexpected error message</response>
    [HttpGet("{pageSize}/{page}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pagination<EmployeeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public async Task<IActionResult> GetPaginatedAsync(
        [FromRoute] int page,
        [FromRoute] int pageSize,
        [FromServices] IMapper mapper)
    {
        try
        {
            var query = new GetEmployeesPaginatedQuery
            {
                Page = page,
                QuantityPerPage = pageSize,
            };
            var result = await mediator.Send(query);

            return result.IsSuccess
                ? Ok(mapper.Map<Pagination<EmployeeDto>>(result.Value))
                : NotFound(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetPaginatedAsync));
            return InternalError();
        }
    }

    private static ObjectResult InternalError() =>
        new("An error has occurred")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
}
