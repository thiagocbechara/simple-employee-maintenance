using FluentAssertions;
using Moq;
using SimpleEmployeeMaintenance.Domain.Employees.Commands.DeleteEmployee;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Tests.Commands;

public class DeleteEmployeeCommandShould
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly DeleteEmployeeCommandHandler _handler;

    public DeleteEmployeeCommandShould()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>(MockBehavior.Strict);

        _handler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteEmployee()
    {
        //Arrange
        var command = new DeleteEmployeeCommand
        {
            Id = Guid.NewGuid()
        };

        _employeeRepositoryMock
            .Setup(r => r.DeleteAsync(It.Is((Guid deletedId) => deletedId == command.Id)))
            .ReturnsAsync(1);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<int>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().Be(1);
        });
        _employeeRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task NotDeleteEmployee_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new DeleteEmployeeCommand();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<int>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}
