using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEmployeeMaintenance.Api.Dtos;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;

namespace SimpleEmployeeMaintenance.Api.Controllers;

[Route("api/employee")]
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
                ? Ok(result)
                : BadRequest(result);

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error has occurred at {0}.", nameof(PostAsync));
            return InternalError();
        }
    }

    private static ObjectResult InternalError() =>
        new("An error has occurred")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
}
