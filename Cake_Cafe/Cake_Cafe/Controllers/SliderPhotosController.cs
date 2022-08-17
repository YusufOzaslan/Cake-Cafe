using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake_Cafe.Data;
using Cake_Cafe.Models;
using Microsoft.AspNetCore.Hosting;

namespace Cake_Cafe.Controllers
{
    public class SliderPhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public SliderPhotosController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: SliderPhotos
        public async Task<IActionResult> Index()
        {
            return View(await _context.SliderPhotos.ToListAsync());
        }

        // GET: SliderPhotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderPhotos = await _context.SliderPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderPhotos == null)
            {
                return NotFound();
            }

            return View(sliderPhotos);
        }

        // GET: SliderPhotos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SliderPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SlideName,Photo")] SliderPhotos sliderPhotos)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\Slide");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Append))
                {
                    files[0].CopyTo(fileStream);
                }

                sliderPhotos.Photo = @"\images\Slide\" + fileName + extension;

                _context.Add(sliderPhotos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sliderPhotos);
        }

        // GET: SliderPhotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderPhotos = await _context.SliderPhotos.FindAsync(id);
            if (sliderPhotos == null)
            {
                return NotFound();
            }
            return View(sliderPhotos);
        }

        // POST: SliderPhotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SlideName,Photo")] SliderPhotos sliderPhotos)
        {
            if (id != sliderPhotos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string webRootPath = _hostEnvironment.WebRootPath;

                    var files = HttpContext.Request.Form.Files;

                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\Slide");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Append))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    sliderPhotos.Photo = @"\images\Slide\" + fileName + extension;

                    _context.Update(sliderPhotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderPhotosExists(sliderPhotos.Id))
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
            return View(sliderPhotos);
        }

        // GET: SliderPhotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderPhotos = await _context.SliderPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderPhotos == null)
            {
                return NotFound();
            }

            return View(sliderPhotos);
        }

        // POST: SliderPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sliderPhotos = await _context.SliderPhotos.FindAsync(id);
            _context.SliderPhotos.Remove(sliderPhotos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderPhotosExists(int id)
        {
            return _context.SliderPhotos.Any(e => e.Id == id);
        }
    }
}
