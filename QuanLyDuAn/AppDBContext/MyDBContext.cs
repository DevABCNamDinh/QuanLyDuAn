using Microsoft.EntityFrameworkCore;
using QuanLyDuAn.Models;

namespace QuanLyDuAn.AppDBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {
            
        }

        public MyDBContext(DbContextOptions options) : base(options)
        {
        }
    
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6F71DIH\\SQLEXPRESS;Initial Catalog=QLDuAn;Integrated Security=True;TrustServerCertificate=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Navigation(u => u.Cart)
                .AutoInclude();

            modelBuilder.Entity<Cart>()
                .Navigation(c => c.User)
                .AutoInclude();

            modelBuilder.Entity<Cart>()
                .Navigation(c => c.CartItems)
                .AutoInclude();

            //modelBuilder.Entity<Book>()
            //    .Navigation(b => b.CartItems)
            //    .AutoInclude();

            modelBuilder.Entity<CartItem>()
                .Navigation(ci => ci.Book)
                .AutoInclude();

            modelBuilder.Entity<CartItem>()
                .Navigation(ci => ci.Cart)
                .AutoInclude();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
              new Book { Id = Guid.NewGuid(), Title = "Không diệt không sinh đừng sợ hãi", Author = "Thích Nhất Hạnh", Description = "MM", Price = 47000, Quantity = 10, ImagePath = "https://cdn1.fahasa.com/media/catalog/product/8/9/8935278607311.jpg" },
              new Book { Id = Guid.NewGuid(), Title = "Hỷ lạc từ tâm", Author = "Desmond Tutu, Douglas Abrams, Tenzin Gyatso", Description = "MM", Price = 97500, Quantity = 10, ImagePath = "https://cdn1.fahasa.com/media/catalog/product/i/m/image_190558.jpg" },
              new Book { Id = Guid.NewGuid(), Title = "Dinh Dưỡng Cho Bệnh Nhân Ung Thư", Author = "Nhiều Tác Giả", Description = "MM", Price = 69000, Quantity = 10, ImagePath = "https://cdn1.fahasa.com/media/catalog/product/_/n/_ng-h_nh-c_ng-b_nh-nh_n-ung-th__-dinh-d_ng-cho-b_nh-nh_n-ung-th_.jpg" },
              new Book { Id = Guid.NewGuid(), Title = "Bản Đồ AI ", Author = "Kate Crawford", Description = "MM", Price = 100000, Quantity = 10, ImagePath = "https://cdn1.fahasa.com/media/catalog/product/n/x/nxbtre_full_04012024_030139.jpg" },
              new Book { Id = Guid.NewGuid(), Title = "Hạnh Phúc Cầm Tay", Author = "Thích Nhất Hạnh", Description = "MM", Price = 65000, Quantity = 10, ImagePath = "https://cdn1.fahasa.com/media/catalog/product/h/a/hanh-phuc-cam-tay---bia-1-_tb-2024_.jpg" }
              );
        }
    }
}
