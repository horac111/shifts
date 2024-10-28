using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shifts.DAL.Models;

namespace Shifts.DAL.Data;
public class ApplicationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> builder = new();
        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Shifts-4f6fa7a9-86f3-4253-a149-303542d1dea5;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new(builder.Options);
    }
}
