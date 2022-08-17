using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake_Cafe.Data;
using Cake_Cafe.Models;

namespace Cake_Cafe.Controllers
{
    public class BestProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BestProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BestProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BestProducts.Include(b => b.Product).Include(c => c.Product.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BestProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProducts = await _context.BestProducts
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestProducts == null)
            {
                return NotFound();
            }

            return View(bestProducts);
        }

        // GET: BestProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductName");
            return View();
        }

        // POST: BestProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId")] BestProducts bestProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductName", bestProducts.ProductId);
            return View(bestProducts);
        }

        // GET: BestProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProducts = await _context.BestProducts.FindAsync(id);
            if (bestProducts == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductName", bestProducts.ProductId);
            return View(bestProducts);
        }

        // POST: BestProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId")] BestProducts bestProducts)
        {
            if (id != bestProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestProductsExists(bestProducts.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductName", bestProducts.ProductId);
            return View(bestProducts);
        }

        // GET: BestProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestProducts = await _context.BestProducts
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestProducts == null)
            {
                return NotFound();
            }

            return View(bestProducts);
        }

        // POST: BestProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestProducts = await _context.BestProducts.FindAsync(id);
            _context.BestProducts.Remove(bestProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestProductsExists(int id)
        {
            return _context.BestProducts.Any(e => e.Id == id);
        }
    }
}
