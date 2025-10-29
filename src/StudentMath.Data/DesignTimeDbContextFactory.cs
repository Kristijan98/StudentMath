using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentMath.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudentMathDbContext>
    {
        public StudentMathDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<StudentMathDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new StudentMathDbContext(optionsBuilder.Options);
        }
    }
}
