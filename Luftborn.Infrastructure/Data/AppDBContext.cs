using Luftborn.Application.Products;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Product> Products => Set<Product>();
    }
}
