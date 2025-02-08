using FluentAssertions;
using Moq;
using SimpleEmployeeMaintenance.Domain.Employees.Queries.GetEmployeeById;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Tests.Employees.Queries;

public class GetEmployeeByIdQueryShould
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly GetEmployeeByIdQueryHandler _handler;

    public GetEmployeeByIdQueryShould()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>(MockBehavior.Strict);

        _handler = new GetEmployeeByIdQueryHandler(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task GetEmployee_When_Found()
    {
        //Arrange
        var id = Guid.NewGuid();
        var command = new GetEmployeeByIdQuery
        {
            Id = id
        };
        var employee = new Employee
        {
            Id = id,
            Name = new("Thiago", "Bechara"),
            Department = "IT",
            Phone = "+5511978547923",
            HireDate = new DateOnly(2022, 03, 01),
            Address = "The only road I've ever known"
        };

        _employeeRepositoryMock
            .Setup(r => r.GetByIdAsync(It.Is((Guid deletedId) => deletedId == command.Id)))
            .ReturnsAsync(employee);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<Employee?>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().Satisfy<Employee>(e =>
            {
                e.Id.Should().Be(employee.Id);
                e.Name.Should().Be(employee.Name);
                e.Department.Should().Be(employee.Department);
                e.Phone.Should().Be(employee.Phone);
                e.HireDate.Should().Be(employee.HireDate);
                e.Address.Should().Be(employee.Address);
            });
        });
        _employeeRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task NotGetEmployee_When_NotFound()
    {
        //Arrange
        var id = Guid.NewGuid();
        var command = new GetEmployeeByIdQuery
        {
            Id = id
        };

        _employeeRepositoryMock
            .Setup(r => r.GetByIdAsync(It.Is((Guid deletedId) => deletedId == command.Id)))
            .ReturnsAsync((Employee?)null);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<Employee?>>(r =>
        {
            r.Value.Should().BeNull();
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Employee not found");
        });
        _employeeRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task NotGetEmployee_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new GetEmployeeByIdQuery();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<Employee?>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}
