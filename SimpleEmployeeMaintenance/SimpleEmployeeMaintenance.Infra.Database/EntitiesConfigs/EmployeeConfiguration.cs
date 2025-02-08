using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEmployeeMaintenance.Infra.Database.Entities;

namespace SimpleEmployeeMaintenance.Infra.Database.EntitiesConfigs;

internal class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeDb>
{
    public void Configure(EntityTypeBuilder<EmployeeDb> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(30);
        builder.Property(e => e.HireDate).IsRequired();
        builder.Property(e => e.Phone).IsRequired().HasMaxLength(15);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(255);

        builder
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);
    }
}
