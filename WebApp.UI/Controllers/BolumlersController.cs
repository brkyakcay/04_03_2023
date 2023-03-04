using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Entities;

namespace WebApp.UI.Controllers
{
    public class BolumlersController : Controller
    {
        private readonly ObsDbContext _context;

        public BolumlersController(ObsDbContext context)
        {
            _context = context;
        }

        // GET: Bolumlers
        public async Task<IActionResult> Index()
        {
            var obsDbContext = _context.Bolumlers.Include(b => b.Ogretmen);
            return View(await obsDbContext.ToListAsync());
        }

        // GET: Bolumlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bolumlers == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumlers
                .Include(b => b.Ogretmen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolumler == null)
            {
                return NotFound();
            }

            return View(bolumler);
        }

        // GET: Bolumlers/Create
        public IActionResult Create()
        {
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenlers, "Id", "Id");
            return View();
        }

        // POST: Bolumlers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,OgretmenId")] Bolumler bolumler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolumler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenlers, "Id", "Id", bolumler.OgretmenId);
            return View(bolumler);
        }

        // GET: Bolumlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bolumlers == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumlers.FindAsync(id);
            if (bolumler == null)
            {
                return NotFound();
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenlers, "Id", "Id", bolumler.OgretmenId);
            return View(bolumler);
        }

        // POST: Bolumlers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,OgretmenId")] Bolumler bolumler)
        {
            if (id != bolumler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolumler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumlerExists(bolumler.Id))
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
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenlers, "Id", "Id", bolumler.OgretmenId);
            return View(bolumler);
        }

        // GET: Bolumlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bolumlers == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumlers
                .Include(b => b.Ogretmen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolumler == null)
            {
                return NotFound();
            }

            return View(bolumler);
        }

        // POST: Bolumlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bolumlers == null)
            {
                return Problem("Entity set 'ObsDbContext.Bolumlers'  is null.");
            }
            var bolumler = await _context.Bolumlers.FindAsync(id);
            if (bolumler != null)
            {
                _context.Bolumlers.Remove(bolumler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumlerExists(int id)
        {
          return (_context.Bolumlers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
