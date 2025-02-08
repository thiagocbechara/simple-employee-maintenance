using Microsoft.EntityFrameworkCore;
using SimpleEmployeeMaintenance.Infra.Database.Entities;

namespace SimpleEmployeeMaintenance.Infra.Database.DataContexts;

internal class AppDataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EmployeeDb> Employees { get; set; }
    public DbSet<DepartmentDb> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);
    }
}
