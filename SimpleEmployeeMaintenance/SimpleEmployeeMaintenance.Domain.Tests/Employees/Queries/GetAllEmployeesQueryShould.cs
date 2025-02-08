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
    public async Task GetAllEmployee()
    {
        //Arrange
        var command = new GetAllEmployeesQuery();

        _employeeRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync([new Employee()]);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<IEnumerable<Employee>>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().HaveCount(1);
        });
        _employeeRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task NotGetAllEmployee_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new GetAllEmployeesQuery();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<IEnumerable<Employee>>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}
