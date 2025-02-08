namespace SimpleEmployeeMaintenance.Domain.ValueObjects;

public record struct Address
{
    public Address(string fullAddress)
    {
        if (fullAddress.Length > 255)
            throw new ArgumentException("Address length cannot be greater then 255", nameof(fullAddress));

        FullAddress = fullAddress;
    }

    public string FullAddress { get; set; }

    #region Conversion Operators
    public static implicit operator string(Address address) => address.FullAddress;
    public static implicit operator Address(string fullAddress) => new(fullAddress);
    #endregion
}
