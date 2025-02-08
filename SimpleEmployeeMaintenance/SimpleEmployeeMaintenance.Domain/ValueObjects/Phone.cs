namespace SimpleEmployeeMaintenance.Domain.ValueObjects;

public record struct Phone
{
    public Phone(string number)
    {
        if (number.Length > 15)
            throw new ArgumentException("Phone length cannot be greater then 15", nameof(number));

        Number = number;
    }

    public string Number { get; set; }

    #region Conversion Operators
    public static implicit operator string(Phone phone) => phone.Number;
    public static implicit operator Phone(string number) => new(number);
    #endregion
}
