using ConcertDB_DarwinOsorioOspina.DAL;
using ConcertDB_DarwinOsorioOspina.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertDB_DarwinOsorioOspina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly BdContext _context;

        public TicketsController(BdContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync(); // Select * From Countries

            if (tickets == null) return NotFound();

            return tickets;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketsById(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == id);
            if (ticket  == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost, ActionName("Post")]
        [Route("Post")]
        public async Task<ActionResult> CreateTicket(Ticket objTicket)
        {
            try
            {
                objTicket.Id = Guid.NewGuid();
                _context.Tickets.Add(objTicket);
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                return Conflict(error.Message);
            }

            return Ok(objTicket);
        }

        [HttpPut, ActionName("Put")]
        [Route("Put/{id}")]
        public async Task<ActionResult> EditTicket(Guid? id, Ticket objTicket)
        {
            try
            {
                if (id != objTicket.Id)
                {
                    return NotFound("Ticket not found");
                }
                else
                {
                    objTicket.UseDate = DateTime.Now;
                    objTicket.IsUsed = true;
                    _context.Tickets.Update(objTicket);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(objTicket);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteTicket(Guid? id)
        {
            if (_context.Tickets == null) return Problem("Entity set 'DataBaseContext.Ticket' is null.");
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.Id == id);
            if (ticket  == null) return NotFound("Ticket not found");
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok(String.Format("This ticket {0} has been delete!", ticket.Id));
        }
    }
}

