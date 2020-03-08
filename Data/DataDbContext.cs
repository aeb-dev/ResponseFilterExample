using Microsoft.EntityFrameworkCore;

namespace ResponseFilterExample
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}