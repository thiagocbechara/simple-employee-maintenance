using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeMaintenance.Api.Dtos;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.DeleteEmployee;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeeById;

namespace SimpleEmployeeMaintenance.Api.Controllers;

[Route("api/employee/")]
[ApiController]
public class EmployeeController(
    ISender mediator,
    ILogger<EmployeeController> logger)
    : ControllerBase
{
    [HttpPost]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        [FromServices] IMapper mapper)
    {
        try
        {
            var query = new GetEmployeeByIdQuery
            {
                Id = id
            };

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            var query = new DeleteEmployeeCommand
            {
                Id = id
            };

            var result = await mediator.Send(query);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.ErrorMesage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetAsync));
            return InternalError();
        }
    }

    [HttpGet]
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
            logger.LogError(exception, "An error has occurred at {0}.", nameof(GetAsync));
            return InternalError();
        }
    }

    private static ObjectResult InternalError() =>
        new("An error has occurred")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
}
