using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Find1.Data;
using Find1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Find1.Controllers
{
    [Authorize()]
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;

        }


        // GET: Ads
        [Authorize()]
        public IActionResult Index()
        {
            var applicationDbContext = _context.Ads.Include(a => a.Category);
            var ads = applicationDbContext.ToList();
            var adFilter = new List<Ad>();

            foreach (var temp in ads)
            {

                if (temp.Owner == User.Identity.Name)
                {

                    adFilter.Add(temp);

                }


                /* var ads = new List<Ad>();

                 ads= (List<Ad>)_context.Ads.Include(a => a.Category);

                 var adFilter = new List<Ad>();

                 foreach(var  temp in ads)
                 {

                     if (temp.Owner == User.Identity.Name)
                     {

                         adFilter.Add(temp);

                     }*/

            }





            return View(adFilter);


            }

            // GET: Ads/Details/5
            [Authorize()]
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

            // GET: Ads/Create

            [Authorize()]
            public IActionResult Create()
            {
                ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
                return View();
            }

            // POST: Ads/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [Authorize()]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(AdViewModel model)
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(model);

                    Ad ad = new Ad
                    {
                        Title = model.Title,
                        // Category = model.Category,
                        CategoryId = model.CategoryId,
                        Location = model.Location,
                        Owner = User.Identity.Name,
                        Mobile = model.Mobile,
                        Price = model.Price,
                        ProfilePicture = uniqueFileName,
                    };
                    _context.Add(ad);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }





            // ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", ad.CategoryId);
            //  return View(ad);


            /*
            // GET: Ads/Edit/5
            [Authorize()]
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ad = await _context.Ads.FindAsync(id);
                if (ad == null)
                {
                    return NotFound();
                }
                ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", ad.CategoryId);
                return View(ad);
            }

            // POST: Ads/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [Authorize()]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId,Location,Owner,Mobile,Price,ProfilePicture,Datetime")] Ad ad)
            {
                if (id != ad.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(ad);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdExists(ad.Id))
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
                ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", ad.CategoryId);
                return View(ad);
            }*/

            // GET: Ads/Delete/5
            [Authorize()]
            public async Task<IActionResult> Delete(int? id)
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

            // POST: Ads/Delete/5
            [Authorize()]
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var ad = await _context.Ads.FindAsync(id);
                _context.Ads.Remove(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool AdExists(int id)
            {
                return _context.Ads.Any(e => e.Id == id);
            }

            private string UploadedFile(AdViewModel model)
            {
                string uniqueFileName = null;

                if (model.AdImage != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AdImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.AdImage.CopyTo(fileStream);
                    }
                }
                return uniqueFileName;
            }

        }
    }
