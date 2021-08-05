using Find1.Data;
using Find1.Models;
using Find1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Find1.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context ,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {


            var applicationDbContext = _context.Ads.Include(a => a.Category);

              return View(await applicationDbContext.OrderByDescending(o=>o.Datetime).ToListAsync());
        }
        



        public IActionResult Adfilter()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adfilter(FilterViewModel model)
        {
           
            if (ModelState.IsValid)
           {
                
                var ap = new List<Ad>();
                if (model.Location == "all")
                  {

                      var applicationDbContext = _context.Ads.Include(a => a.Category).Where(a => (a.CategoryId == model.CategoryId) && (a.Price >= model.MinPrice && a.Price <= model.MaxPrice));                //(b.CategoryId == model.CategoryId) &&

                      ap = applicationDbContext.ToList();
                      ap.OrderByDescending(a => a.Datetime);

                      return View("Index", ap);
                  }
                  else
                  {


                var applicationDbContext = _context.Ads.Include(a => a.Category).Where(a => (a.Location == model.Location) && (a.CategoryId == model.CategoryId)&& ((a.Price >= model.MinPrice) && (a.Price <= model.MaxPrice)));                //(b.CategoryId == model.CategoryId) &&

                    ap = applicationDbContext.ToList();
                    ap.OrderByDescending(a => a.Datetime);

                    return View("Index", ap);

                }
            }

           return View();

        }












        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
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
}
