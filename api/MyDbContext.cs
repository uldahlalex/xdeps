using Microsoft.EntityFrameworkCore;
using serversidevalidation;

public class MyDbContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}