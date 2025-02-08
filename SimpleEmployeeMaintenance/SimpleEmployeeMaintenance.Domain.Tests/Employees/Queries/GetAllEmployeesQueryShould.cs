using FluentAssertions;
using Moq;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetAllEmployees;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Tests.Employees.Queries;

public class GetAllEmployeesQueryShould
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly GetAllEmployeesQueryHandler _handler;

    public GetAllEmployeesQueryShould()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>(MockBehavior.Strict);

        _handler = new GetAllEmployeesQueryHandler(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteEmployee()
    {
        //Arrange
        var command = new GetAllEmployeesQuery
        {
            Page = 1,
            QuantityPerPage = 10,
        };

        _employeeRepositoryMock
            .Setup(r => r.GetPaginatedAsync(command.Page, command.QuantityPerPage))
            .ReturnsAsync(new Pagination<Employee>()
            {
                CurrentPage = 1,
                ResultsPerPage = 10,
                TotalPages = 1,
                TotalResults = 1,
                Results = [new Employee()]
            });

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<Pagination<Employee>>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().Satisfy<Pagination<Employee>>(p =>
            {
                p.CurrentPage.Should().Be(command.Page);
                p.ResultsPerPage.Should().Be(command.QuantityPerPage);
                p.TotalPages.Should().Be(1);
                p.TotalResults.Should().Be(1);
                p.Results.Should().HaveCount(1);
            });
        });
        _employeeRepositoryMock.Verify(r => r.GetPaginatedAsync(command.Page, command.QuantityPerPage), Times.Once);
    }

    [Fact]
    public async Task NotDeleteEmployee_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new GetAllEmployeesQuery();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<Pagination<Employee>>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}
