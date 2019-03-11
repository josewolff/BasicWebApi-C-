
using Microsoft.EntityFrameworkCore;

namespace TestVS.Models
{
    public class DbConnection : DbContext
    {
        // Reference our users table using this
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=./Testing.db");
        }
    }
}
