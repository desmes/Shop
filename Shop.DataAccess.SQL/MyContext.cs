using Shop.Core.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Shop.DataAccess.SQL
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }

}