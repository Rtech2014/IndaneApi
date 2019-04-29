﻿using System;
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
    public class FullsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FullsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fulls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Full>>> GetFulls()
        {
            var applicationDbContext = await _context.Fulls.Include(f => f.DeliveryPersonDetail).Include(f => f.Product).ToListAsync();

            foreach (var item in applicationDbContext)
            {
                item.CashToBeRecevied = item.FullCount * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
            }
            return applicationDbContext;
        }

        // GET: api/Fulls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Full>> GetFull(int id)
        {
            var full = await _context.Fulls.FindAsync(id);

            if (full == null)
            {
                return NotFound();
            }

            return full;
        }

        // PUT: api/Fulls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFull(int id, Full full)
        {
            if (id != full.Id)
            {
                return BadRequest();
            }

            _context.Entry(full).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FullExists(id))
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

        // POST: api/Fulls
        [HttpPost]
        public async Task<ActionResult<Full>> PostFull(Full full)
        {

            _context.Fulls.Add(full);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFull", new { id = full.Id }, full);
        }

        // DELETE: api/Fulls/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Full>> DeleteFull(int id)
        {
            var full = await _context.Fulls.FindAsync(id);
            if (full == null)
            {
                return NotFound();
            }

            _context.Fulls.Remove(full);
            await _context.SaveChangesAsync();

            return full;
        }

        private bool FullExists(int id)
        {
            return _context.Fulls.Any(e => e.Id == id);
        }
    }
}
