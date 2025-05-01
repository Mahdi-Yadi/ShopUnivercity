using Microsoft.EntityFrameworkCore;
using ShopUnivercity.Web.Models.Entities.Account;
using ShopUnivercity.Web.Models.Entities.Orders;
using ShopUnivercity.Web.Models.Entities.Products;
using ShopUnivercity.Web.Models.Entities.Site;

namespace ShopUnivercity.Web.Data;
public class SiteDBContext : DbContext
{

    public SiteDBContext(DbContextOptions<SiteDBContext> options) :base (options)
    {
        
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Product> Product { get; set; }

    public DbSet<SiteSettings> SiteSettings { get; set; }


}