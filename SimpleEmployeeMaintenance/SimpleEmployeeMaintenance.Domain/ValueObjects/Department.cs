namespace SimpleEmployeeMaintenance.Domain.ValueObjects;

public record struct Department
{
    public Department(string name)
    {
        if (name.Length > 50)
            throw new ArgumentException("Department length cannot be greater then 50", nameof(name));

        Name = name;
    }

    public string Name { get; set; }

    #region Conversion Operators
    public static implicit operator string(Department department) => department.Name;
    public static implicit operator Department(string name) => new(name);
    #endregion
}
