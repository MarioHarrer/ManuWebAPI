using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManuWebAPI.Models;

namespace ManuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpielersController : ControllerBase
    {
        private readonly SpielerContext _context;

        public SpielersController(SpielerContext context)
        {
            _context = context;
        }

        // GET: api/Spielers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spieler>>> GetSpielers()
        {
            return await _context.Spielers.ToListAsync();
        }

        // GET: api/Spielers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spieler>> GetSpieler(long id)
        {
            var spieler = await _context.Spielers.FindAsync(id);

            if (spieler == null)
            {
                return NotFound();
            }

            return spieler;
        }

        // PUT: api/Spielers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpieler(long id, Spieler spieler)
        {
            if (id != spieler.Id)
            {
                return BadRequest();
            }

            _context.Entry(spieler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpielerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Spielers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spieler>> PostSpieler(Spieler spieler)
        {
            _context.Spielers.Add(spieler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpieler", new { id = spieler.Id }, spieler);
        }

        // DELETE: api/Spielers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpieler(long id)
        {
            var spieler = await _context.Spielers.FindAsync(id);
            if (spieler == null)
            {
                return NotFound();
            }

            _context.Spielers.Remove(spieler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpielerExists(long id)
        {
            return _context.Spielers.Any(e => e.Id == id);
        }
    }
}
