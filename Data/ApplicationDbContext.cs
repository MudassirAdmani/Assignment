using ImgCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ImgCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
