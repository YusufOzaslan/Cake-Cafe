﻿using Cake_Cafe.Data;
using Cake_Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cake_Cafe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var db = _context.SliderPhotos;
            var dbBestProducts = _context.BestProducts.Include(p=> p.Product);
            var model = new ProductDTO { SliderPhotosEnumerable = db, BestProductsEnumerable = dbBestProducts };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ProductDTO // Data Transfer Object
    {
        public IEnumerable<SliderPhotos> SliderPhotosEnumerable { get; set; }
        public IEnumerable<BestProducts> BestProductsEnumerable { get; set; }
    }
}
