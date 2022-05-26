using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Ticket> Tickets { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}