using Microsoft.EntityFrameworkCore;
using StockPlaform.Models;

namespace StockPlaform.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options):base(options)
        {
       
            
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
