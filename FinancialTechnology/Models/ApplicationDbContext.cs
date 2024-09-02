using Microsoft.EntityFrameworkCore;

namespace FinancialTechnology.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }
    }
}