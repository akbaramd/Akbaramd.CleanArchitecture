using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ACA.Infrastructure.Data
{
    public class ACADbContextFactory : IDesignTimeDbContextFactory<ACADbContext>
    {
        public ACADbContext CreateDbContext(string[] args)
        {
            var connectionString =
                "Data Source=.;initial catalog=ACA;Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True;";
            var builder = new DbContextOptionsBuilder<ACADbContext>();
            builder.UseSqlServer(connectionString);
            var options = builder.Options;

            return new ACADbContext(options);
        }
    }
}