using Cake_Cafe.Data;
using Cake_Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Cake_Cafe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _context = context;

            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var db = _context.SliderPhotos;
            var dbBestProducts = _context.BestProducts.Include(p=> p.Product);
            var model = new ProductDTO { SliderPhotosEnumerable = db, BestProductsEnumerable = dbBestProducts };

            return View(model);
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
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
