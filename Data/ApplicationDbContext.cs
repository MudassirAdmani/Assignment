using Microsoft.EntityFrameworkCore;
using NetCoreAssignment.Models.Entities;

namespace NetCoreAssignment.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
