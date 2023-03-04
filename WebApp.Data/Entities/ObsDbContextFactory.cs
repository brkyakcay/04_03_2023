using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data.Entities;

public class ObsDbContextFactory : IDesignTimeDbContextFactory<ObsDbContext>
{
    public ObsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ObsDbContext>();

        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Obs8;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=True;");

        return new ObsDbContext(optionsBuilder.Options);
    }
}