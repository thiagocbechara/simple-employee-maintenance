using AutoMapper;
using FluentAssertions;
using Moq;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.CreateEmployee;
using SimpleEmployeeMaintenance.Domain.Entities;
using SimpleEmployeeMaintenance.Domain.MapperProfiles;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Tests.Commands;

public class CreateEmployeeCommandShould
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly CreateEmployeeCommandHandler _handler;

    public CreateEmployeeCommandShould()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>(MockBehavior.Strict);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new DomainProfile())));

        _handler = new CreateEmployeeCommandHandler(_employeeRepositoryMock.Object, mapper);
    }

    [Fact]
    public async Task CreateEmployee_When_Data_IsValid()
    {
        //Arrange
        var command = new CreateEmployeeCommand
        {
            FirstName = "Thiago",
            LastName = "Bechara",
            Department = "IT",
            Phone = "+5511978547923",
            HireDate = new DateOnly(2022, 03, 01),
            Address = "The only road I've ever known"
        };
        var newId = Guid.NewGuid();

        _employeeRepositoryMock
            .Setup(r => r.SaveAsync(It.IsAny<Employee>()))
            .ReturnsAsync(newId);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<Guid>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().Be(newId);
        });
        _employeeRepositoryMock.Verify(r => r.SaveAsync(It.IsAny<Employee>()), Times.Once);
    }

    [Fact]
    public async Task NotCreateEmployee_When_Data_IsInValid()
    {
        //Arrange
        var command = new CreateEmployeeCommand
        {
            FirstName = "Thiago",
            LastName = "Bechara",
            Address = "The only road I've ever known"
        };
        
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<Guid>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Employee data is not valid");
        });
    }

    [Fact]
    public async Task NotCreateEmployee_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new CreateEmployeeCommand();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<Guid>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}