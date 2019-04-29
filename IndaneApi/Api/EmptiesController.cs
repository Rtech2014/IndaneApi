using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndaneApi.Data;
using IndaneApi.Models;

namespace IndaneApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmptiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmptiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Empties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empty>>> GetEmpties()
        {
            return await _context.Empties.Include(s => s.DeliveryPersonDetail)
                                            .Include(s => s.Product)
                                            .ToListAsync();
        }

        // GET: api/Empties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empty>> GetEmpty(int id)
        {
            var empty = await _context.Empties.FindAsync(id);

            if (empty == null)
            {
                return NotFound();
            }

            return empty;
        }

        // PUT: api/Empties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpty(int id, Empty empty)
        {
            if (id != empty.Id)
            {
                return BadRequest();
            }

            _context.Entry(empty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmptyExists(id))
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

        // POST: api/Empties
        [HttpPost]
        public async Task<ActionResult<Empty>> PostEmpty(Empty empty)
        {
            var product = empty.ProductId;
            var delivery = empty.DeliveryPersonId;
            var takeCount = empty.In_No;
            var datestamp = DateTime.Today.Date;
            var fullDetail = _context.Fulls.Where(s => s.DeliveryPersonId == delivery && s.Out_No == takeCount && s.ProductId == product && s.TimeStamp.Date == datestamp).FirstOrDefault();

            if (fullDetail.FullCount >= empty.EmptyNo)
            {
                empty.TimeStamp = DateTime.Now;
                empty.FullId = fullDetail.Id;
                _context.Add(empty);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEmpty", new { id = empty.Id }, empty);
            }
            return CreatedAtAction("GetEmpty", new { id = empty.Id }, empty);
        }

        // DELETE: api/Empties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Empty>> DeleteEmpty(int id)
        {
            var empty = await _context.Empties.FindAsync(id);
            if (empty == null)
            {
                return NotFound();
            }

            _context.Empties.Remove(empty);
            await _context.SaveChangesAsync();

            return empty;
        }

        private bool EmptyExists(int id)
        {
            return _context.Empties.Any(e => e.Id == id);
        }
    }
}
