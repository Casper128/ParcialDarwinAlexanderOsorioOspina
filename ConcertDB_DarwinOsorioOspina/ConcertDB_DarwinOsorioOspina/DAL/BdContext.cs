using ConcertDB_DarwinOsorioOspina.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConcertDB_DarwinOsorioOspina.DAL
{
    public class BdContext: DbContext
    {
        public BdContext(DbContextOptions<BdContext> options):base(options) {
            
        }
        public DbSet<Ticket> Tickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.EntranceGate)
                .IsRequired(false); // Configurar como no requerido (acepta valores nulos)

            modelBuilder.Entity<Ticket>()
                .Property(t => t.UseDate)
                .IsRequired(false); // Configurar como no requerido (acepta valores nulos)

            // Otros mapeos y configuraciones...

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.Id)
                .IsUnique();
        }
    }
}
