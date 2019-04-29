using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndaneApi.Data;
using IndaneApi.Models;

namespace IndaneApi.Controllers
{
    public class EmptiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmptiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Empties.Include(e => e.DeliveryPersonDetail).Include(e => e.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Empties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties
                .Include(e => e.DeliveryPersonDetail)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empty == null)
            {
                return NotFound();
            }

            return View(empty);
        }

        // GET: Empties/Create
        public IActionResult Create()
        {
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Empties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,DeliveryPersonId,TimeStamp,In_No,EmptyNo,NewConnection,EmptyPending,ReturnedFull,CashRecevied")] Empty empty)
        {

            if (ModelState.IsValid)
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
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    RedirectToAction("ErrorInfo", "Empties");
                    //ViewData["ErrorInfo"] = "Refill Cylinder count exceeds the limit.please enter correct value.";
                }

            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // GET: Empties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties.FindAsync(id);
            if (empty == null)
            {
                return NotFound();
            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // POST: Empties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,DeliveryPersonId,TimeStamp,In_No,EmptyNo,NewConnection,EmptyPending,ReturnedFull,CashRecevied")] Empty empty)
        {
            if (id != empty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmptyExists(empty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // GET: Empties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties
                .Include(e => e.DeliveryPersonDetail)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empty == null)
            {
                return NotFound();
            }

            return View(empty);
        }

        // POST: Empties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empty = await _context.Empties.FindAsync(id);
            _context.Empties.Remove(empty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmptyExists(int id)
        {
            return _context.Empties.Any(e => e.Id == id);
        }
    }
}
