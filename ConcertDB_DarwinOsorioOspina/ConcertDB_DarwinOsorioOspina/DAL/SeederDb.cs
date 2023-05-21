using System.Diagnostics.Metrics;
using ConcertDB_DarwinOsorioOspina.DAL.Entity;

namespace ConcertDB_DarwinOsorioOspina.DAL
{
    public class SeederDb
    {
        private readonly BdContext _context;
        public SeederDb(BdContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CreateTicketsAsync();
            await _context.SaveChangesAsync();
        }

        private async Task CreateTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                int i = 0;
                while (i < 5000)
                {
                    await _context.Tickets.AddAsync(new Ticket
                    {
                        Id = Guid.NewGuid(),
                        EntranceGate = null,
                        IsUsed = false,
                        UseDate = null,
                    });
                    i++;
                }
            }
        }
    }
}
