using ConcertDB_DarwinOsorioOspina.DAL;
using ConcertDB_DarwinOsorioOspina.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var citys = await _context.Tickets.ToListAsync(); // Select * From Countries

            if (citys == null) return NotFound();

            return citys;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<City>> GetCityById(Guid? id)
        {
            var citys = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id); //Select * From Countries Where Id = "..."

            if (citys == null) return NotFound();

            return Ok(citys);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountry(City citys)
        {
            try
            {
                citys.Id = Guid.NewGuid();
                citys.CreatedDate = DateTime.Now;

                _context.Cities.Add(citys);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el Insert Into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", citys.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(citys);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditCountry(Guid? id, City citys)
        {
            try
            {
                if (id != citys.Id) return NotFound("Country not found");

                citys.ModifiedDate = DateTime.Now;

                _context.Cities.Update(citys);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el Update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", citys.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(citys);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCities(Guid? id)
        {
            if (_context.Countries == null) return Problem("Entity set 'DataBaseContext.Countries' is null.");
            var citys = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);

            if (citys == null) return NotFound("Country not found");

            _context.Countries.Remove(citys);
            await _context.SaveChangesAsync(); //Hace las veces del Delete en SQL

            return Ok(String.Format("El país {0} fue eliminado!", citys.Name));
        }
    }
}

