
using Microsoft.EntityFrameworkCore;

namespace TestVS.Models
{
    public class UsersDb : DbContext
    {
        // Reference our users table using this
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=./Testing.db");
        }
    }
}
