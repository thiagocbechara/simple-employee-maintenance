using FluentAssertions;
using Moq;
using SimpleEmployeeMaintenance.Domain.Departments.Queries;
using SimpleEmployeeMaintenance.Domain.Models;
using SimpleEmployeeMaintenance.Domain.Repositories;

namespace SimpleEmployeeMaintenance.Domain.Tests.Departments.Queries;

public class GetAllDepartmentsQueryShould
{
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly GetAllDepartmentsQueryHandler _handler;

    public GetAllDepartmentsQueryShould()
    {
        _departmentRepositoryMock = new Mock<IDepartmentRepository>(MockBehavior.Strict);

        _handler = new GetAllDepartmentsQueryHandler(_departmentRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllDepartment()
    {
        //Arrange
        var command = new GetAllDepartmentsQuery();

        _departmentRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(["IT", "HR"]);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().Satisfy<Result<IEnumerable<string>>>(r =>
        {
            r.IsSuccess.Should().BeTrue();
            r.Value.Should().HaveCount(2);
        });
        _departmentRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task NotGetAllDepartment_When_Cancellation_WasRequested()
    {
        //Arrange
        var command = new GetAllDepartmentsQuery();
        var cancellationToken = new CancellationToken(true);

        //Act
        var result = await _handler.Handle(command, cancellationToken);

        //Assert
        result.Should().Satisfy<Result<IEnumerable<string>>>(r =>
        {
            r.IsSuccess.Should().BeFalse();
            r.ErrorMesage.Should().Be("Cancellation was requested");
        });
    }
}
