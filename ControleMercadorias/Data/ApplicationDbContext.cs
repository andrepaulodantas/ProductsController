
using ControleMercadorias.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadorias.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ProductItem> Products { get; set; }
        public DbSet<ProductEntry> ProductEntries { get; set; }
        public DbSet<ProductExit> ProductExits { get; set; }
    }
}

