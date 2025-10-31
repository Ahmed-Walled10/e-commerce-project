using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_project.Modles
{
    public class sql_e_commerce_DB : IdentityDbContext<Users>
    {
        public sql_e_commerce_DB(DbContextOptions<sql_e_commerce_DB> options) : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_item> Cart_items { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_item> Order_items { get; set; }
        public DbSet<Payment_details> Payment_details { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Product_skus> product_Skus { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishList_products> WishList_Products { get; set; }
        public DbSet<Products_categories> Products_Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🧍 User ↔ Address (1-to-many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧍 User ↔ Wishlist (1-to-1)
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Wishlist)
                .WithOne(w => w.User)
                .HasForeignKey<Wishlist>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧍 User ↔ Cart (1-to-1)
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧍 User ↔ Order (1-to-many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧺 Wishlist ↔ Product (many-to-many)
            modelBuilder.Entity<WishList_products>()
                .HasKey(wp => new { wp.Wishlist_Id, wp.Product_Id });

            modelBuilder.Entity<WishList_products>()
                .HasOne(wp => wp.Wishlist)
                .WithMany(w => w.WishList_Products)
                .HasForeignKey(wp => wp.Wishlist_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishList_products>()
                .HasOne(wp => wp.Product)
                .WithMany(p => p.WishList_Products)
                .HasForeignKey(wp => wp.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🛒 Cart ↔ Cart_item (1-to-many)
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Cart_item)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.Cart_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🏷️ Product ↔ Categories (many-to-many)
            modelBuilder.Entity<Products_categories>()
                .HasKey(pc => new { pc.Product_Id, pc.Category_Id });

            modelBuilder.Entity<Products_categories>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Products_Categories)
                .HasForeignKey(pc => pc.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Products_categories>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Products_Categories)
                .HasForeignKey(pc => pc.Category_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧾 Product ↔ Cart_item (many-to-many)
            modelBuilder.Entity<Cart_item>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.Cart_Items)
                .HasForeignKey(ci => ci.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 📦 Product_skus ↔ Product (many skus to one product)
            modelBuilder.Entity<Product_skus>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.Product_Skus)
                .HasForeignKey(ps => ps.product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧾 Product ↔ Order_item (many-to-many)
            modelBuilder.Entity<Order_item>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.Order_Item)
                .HasForeignKey(oi => oi.product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧾 Order ↔ Order_item (1-to-many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Order_Item)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 💳 Payment_details ↔ Order (1-to-1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment_Details)
                .WithOne(pd => pd.Order)
                .HasForeignKey<Payment_details>(pd => pd.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
