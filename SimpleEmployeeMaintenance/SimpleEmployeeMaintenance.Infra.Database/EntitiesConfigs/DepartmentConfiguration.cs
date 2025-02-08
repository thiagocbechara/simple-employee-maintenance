using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEmployeeMaintenance.Infra.Database.Entities;

namespace SimpleEmployeeMaintenance.Infra.Database.EntitiesConfigs;

internal class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentDb>
{
    public void Configure(EntityTypeBuilder<DepartmentDb> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(d => d.Id);

        builder.HasIndex(d => d.Name)
            .IsUnique();

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50)
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
    }
}
