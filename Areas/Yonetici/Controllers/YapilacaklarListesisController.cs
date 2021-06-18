using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeYonetim.Data;
using ProjeYonetim.Models;

namespace ProjeYonetim.Areas.Yonetici.Controllers
{
   
    public class YapilacaklarListesisController : TemelController
    {
        private readonly ApplicationDbContext _context;

        public YapilacaklarListesisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.YapilacaklarListesis.Include(y => y.Personel);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Detay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yapilacaklarListesi = await _context.YapilacaklarListesis
                .Include(y => y.Personel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (yapilacaklarListesi == null)
            {
                return NotFound();
            }

            return View(yapilacaklarListesi);
        }

       
        public IActionResult Ekle()
        {
            ViewData["PersonelID"] = new SelectList(_context.Personels, "ID", "AdSoyad");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(YapilacaklarListesi yapilacaklarListesi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yapilacaklarListesi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonelID"] = new SelectList(_context.Personels, "ID", "Ad", yapilacaklarListesi.PersonelID);
            return View(yapilacaklarListesi);
        }

      
        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yapilacaklarListesi = await _context.YapilacaklarListesis.FindAsync(id);
            if (yapilacaklarListesi == null)
            {
                return NotFound();
            }
            ViewData["PersonelID"] = new SelectList(_context.Personels, "ID", "Ad", yapilacaklarListesi.PersonelID);
            return View(yapilacaklarListesi);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, [Bind("ID,Baslik,Gorev,BaslamaZamani,BitisZamanı,Oncelik,EkAlan,PersonelID")] YapilacaklarListesi yapilacaklarListesi)
        {
            if (id != yapilacaklarListesi.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yapilacaklarListesi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YapilacaklarListesiExists(yapilacaklarListesi.ID))
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
            ViewData["PersonelID"] = new SelectList(_context.Personels, "ID", "Ad", yapilacaklarListesi.PersonelID);
            return View(yapilacaklarListesi);
        }

     
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yapilacaklarListesi = await _context.YapilacaklarListesis
                .Include(y => y.Personel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (yapilacaklarListesi == null)
            {
                return NotFound();
            }

            return View(yapilacaklarListesi);
        }

       
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var yapilacaklarListesi = await _context.YapilacaklarListesis.FindAsync(id);
            _context.YapilacaklarListesis.Remove(yapilacaklarListesi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YapilacaklarListesiExists(int id)
        {
            return _context.YapilacaklarListesis.Any(e => e.ID == id);
        }
    }
}
