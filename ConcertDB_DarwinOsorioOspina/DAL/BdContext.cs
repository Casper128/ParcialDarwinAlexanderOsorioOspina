using ConcertDB_DarwinOsorioOspina.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConcertDB_DarwinOsorioOspina.DAL
{
    public class BdContext: DbContext
    {
        public BdContext(DbContextOptions<BdContext> options):base(options) {
            
        }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) { 
            base.OnModelCreating (modelBuilder);
            modelBuilder.Entity<Ticket> ().HasIndex(c => c.Id).IsUnique();
        }
    }
}
