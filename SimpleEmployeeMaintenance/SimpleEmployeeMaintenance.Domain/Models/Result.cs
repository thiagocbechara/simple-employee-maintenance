namespace SimpleEmployeeMaintenance.Domain.Models;

public record Result<TValue>
{
    public TValue Value { get; private set; } = default!;
    public bool IsSuccess { get; private set; }
    public string? ErrorMesage { get; private set; }

    public static Result<TValue> ResultError(string errorMesage) =>
        new()
        {
            IsSuccess = false,
            ErrorMesage = errorMesage
        };

    public static Result<TValue> ResultSuccess(TValue value) =>
        new()
        {
            Value = value,
            IsSuccess = true
        };
}
