using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cake_Cafe.Data;
using Microsoft.EntityFrameworkCore;

namespace Cake_Cafe.Controllers
{
    public class CakeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CakeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product;
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
