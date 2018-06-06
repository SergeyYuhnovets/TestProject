using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertApp.Data;
using ConcertApp.Models;

namespace ConcertApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConcertEvent.Include(c => c.EventType).Include(c => c.Location);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertEvent = await _context.ConcertEvent
                .Include(c => c.EventType)
                .Include(c => c.Location)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (concertEvent == null)
            {
                return NotFound();
            }

            return View(concertEvent);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["EventTypeID"] = new SelectList(_context.EventType, "ID", "ID");
            ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Musician,Tickets,Date,LocationID,EventTypeID,Price")] ConcertEvent concertEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concertEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventTypeID"] = new SelectList(_context.EventType, "ID", "ID", concertEvent.EventTypeID);
            ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name", concertEvent.LocationID);
            return View(concertEvent);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertEvent = await _context.ConcertEvent.SingleOrDefaultAsync(m => m.ID == id);
            if (concertEvent == null)
            {
                return NotFound();
            }
            ViewData["EventTypeID"] = new SelectList(_context.EventType, "ID", "ID", concertEvent.EventTypeID);
            ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name", concertEvent.LocationID);
            return View(concertEvent);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Musician,Tickets,Date,LocationID,EventTypeID,Price")] ConcertEvent concertEvent)
        {
            if (id != concertEvent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertEventExists(concertEvent.ID))
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
            ViewData["EventTypeID"] = new SelectList(_context.EventType, "ID", "ID", concertEvent.EventTypeID);
            ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name", concertEvent.LocationID);
            return View(concertEvent);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertEvent = await _context.ConcertEvent
                .Include(c => c.EventType)
                .Include(c => c.Location)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (concertEvent == null)
            {
                return NotFound();
            }

            return View(concertEvent);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concertEvent = await _context.ConcertEvent.SingleOrDefaultAsync(m => m.ID == id);
            _context.ConcertEvent.Remove(concertEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertEventExists(int id)
        {
            return _context.ConcertEvent.Any(e => e.ID == id);
        }
    }
}
