namespace SimpleEmployeeMaintenance.Domain.ValueObjects;

public record struct Name
{
    public Name(string first, string last)
    {
        if(first.Length > 30)
            throw new ArgumentException("FirstName length cannot be greater then 30", nameof(first));
        if(last.Length > 30)
            throw new ArgumentException("LastName length cannot be greater then 30", nameof(last));

        First = first;
        Last = last;
    }

    public string First { get; set; }
    public string Last { get; set; }

    public override readonly string ToString() => $"{First} {Last}";

    #region Conversion Operators
    public static implicit operator string(Name name) => name.ToString();
    public static implicit operator Name(string fullname)
    {
        var names = fullname.Split(' ');
        if(names.Length > 0)
            return new(names[0], names[^1]);

        return new (fullname, string.Empty);
    }
    #endregion
}
