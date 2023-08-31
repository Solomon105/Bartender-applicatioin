using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bartender_applicatioin.Models
{

    public class AppDbContext : DbContext
    {
        public DbSet<CocktailMenu> CocktailMenus { get; set; }
        public DbSet<CocktailOrder> CocktailOrders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
