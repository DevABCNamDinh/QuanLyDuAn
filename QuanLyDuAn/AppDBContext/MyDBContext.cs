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
    
        public DbSet<User> users { get; set; }
    }
}
