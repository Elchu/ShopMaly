using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=ShopDatabase") 
            {
            }

        public DbSet<Product> Product{ get; set; }
    }
}