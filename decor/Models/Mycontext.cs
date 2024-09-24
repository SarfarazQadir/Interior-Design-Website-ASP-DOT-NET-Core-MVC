using DecorVista.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace decor.Models
{
    public class Mycontext : DbContext
    {
        public Mycontext(DbContextOptions<Mycontext> options) : base(options)
        {

        }
        public DbSet<User> tbl_users { get; set; }

        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        public DbSet<Category> tbl_category { get; set; }
        public DbSet<Designer> tbl_designer { get; set; }
        public DbSet<Blogs> tbl_bolg { get; set; }

        public DbSet<Cart> tbl_cart { get; set; }

        public DbSet<Checkout> tbl_checkout { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Product)
                .HasForeignKey(p => p.category_id);
        }
    }
}
