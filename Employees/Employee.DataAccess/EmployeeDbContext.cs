using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
    }
}

/*
 *
 *
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Employee.DataAccess
{
    public class DbContextFactory: IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        private readonly IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EmployeeDbContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetConnectionString("SQLServer");

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDbContext(optionsBuilder.Options);
        }
    }
}
 */