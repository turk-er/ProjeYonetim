using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeYonetim.Data;
using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Areas.Yonetici.Controllers
{
    public class ProjeController : TemelController
    {
        private readonly ApplicationDbContext _context;
        public ProjeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
                 var projes =  (_context.Projes.Include(x => x.PersonelProjes).ThenInclude(x => x.Personel)).ToList();

           foreach (var item in projes)
            {
                double gider = 0;
                foreach (var it in item.PersonelProjes)
                {
                    var person = _context.Personels.FirstOrDefault(x => x.ID == it.PersonelID);
                      gider += +person.Maas;

                }
                  item.NetKar = item.ProjeGeliri - (gider * item.Ay);
            }
            return View(projes);
        }
        public IActionResult Detay(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var proje = _context.Projes.Include(x => x.PersonelProjes).ThenInclude(x => x.Personel).FirstOrDefault(m => m.ID == Id);
            double gider = 0;

            foreach (var item in proje.PersonelProjes)
            {
                var person = _context.Personels.FirstOrDefault(x => x.ID == item.PersonelID);
                gider += +person.Maas;
            }
            proje.NetKar = proje.ProjeGeliri - (gider * proje.Ay);

            var gecensure = ((DateTime.Now - proje.BasTarihi).TotalDays) / 30;

            int ilerleme = Convert.ToInt32((gecensure * 100) / proje.Ay);
           
            TempData["Ilerleme"] = ilerleme;

            if (proje == null)
            {
                return NotFound();
            }
            return View(proje);

        }
        public IActionResult Ekle()
        {
           
            ViewBag.Per = new SelectList(_context.Personels ,nameof(Personel.ID),nameof(Personel.AdSoyad));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(Proje proje)
        {

            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    TimeSpan zaman = proje.BitTarihi - proje.BasTarihi;
                    proje.Ay = Math.Round((zaman.TotalDays / 30.5), 2);

                    await _context.Projes.AddAsync(proje);
                    int sonuc = await _context.SaveChangesAsync();

                    foreach (var item in proje.PersonelList)
                    {
                        PersonelProje pp = new PersonelProje()
                        {
                            ProjeID = proje.ID,
                            PersonelID = item
                        };
                        await _context.PersonelProjes.AddAsync(pp);
                    }
                    sonuc = await _context.SaveChangesAsync();

                    if (sonuc >= 1)
                    {
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        throw new Exception("Kaydedilemedi!");
                }
            }

            ViewBag.Per = new SelectList(_context.Personels, nameof(Personel.ID), nameof(Personel.AdSoyad));
            return View(proje);
        }

        public async Task<IActionResult> Duzenle(int? id)
        {

            if (id==null)
            {
                return RedirectToAction(nameof(Index));
            };
            var proje = await _context.Projes.FirstOrDefaultAsync(x=>x.ID==id);
            var pp = await _context.PersonelProjes.Where(x => x.ProjeID == id).ToListAsync();

            if (proje==null)
            {
                return NotFound(proje);
            }
            ViewBag.Per = new SelectList(_context.Personels, nameof(Personel.ID), nameof(Personel.AdSoyad));
            return View(proje);
        }

        [HttpPost]
        public async Task<IActionResult> Duzenle(int id,Proje proje)
        {
            var _proje = await _context.Projes.FirstOrDefaultAsync(x => x.ID == id);

            if (ModelState.IsValid)
            {
                int sonuc = 0;

                using (var transaction = _context.Database.BeginTransaction())
                { 
                    
                    _proje.ProjeAdi = proje.ProjeAdi;
                    _proje.ProjeGeliri = proje.ProjeGeliri;
                    _proje.BasTarihi = proje.BasTarihi;
                    _proje.BitTarihi = proje.BitTarihi;
                   
                  
                    TimeSpan zaman = _proje.BitTarihi - _proje.BasTarihi;
                    _proje.Ay = Math.Round((zaman.TotalDays / 30.5), 2);

                    List<PersonelProje> pep = _context.PersonelProjes.Where(x => x.ProjeID == id).ToList();

                    if (proje.PersonelList==null)
                    {
                        _proje.PersonelProjes = pep;
                    }
                    else
                    {
                        _context.PersonelProjes.RemoveRange(pep);

                         foreach (var item in proje.PersonelList)
                         {
                               PersonelProje pp = new PersonelProje()
                              {
                                     ProjeID = proje.ID,
                                    PersonelID = item
                              };
                         _context.PersonelProjes.AddRange(pp);
                        }
                    }
                    sonuc = await _context.SaveChangesAsync();

                    if (sonuc >= 1)
                    {
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        throw new Exception("Kaydedilemedi!");
                }
            }
            ViewBag.Per = new SelectList(_context.Personels, nameof(Personel.ID), nameof(Personel.AdSoyad));

            return View(proje);
        }
        public async Task<IActionResult> Sil(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var projes = await _context.Projes.FirstOrDefaultAsync(x => x.ID == id);

            if (projes==null)
            {
                return NotFound();
            }
            return View(projes);
           
        }

        [HttpPost]
        public async Task<IActionResult> Sil(int id,Proje proje)
        {
            var projes = await _context.Projes.FirstOrDefaultAsync(x => x.ID == id);

            _context.Projes.Remove(projes);
            int sonuc = await _context.SaveChangesAsync();
            if (sonuc>=1)
            {
               return RedirectToAction(nameof(Index));
            }

            return View(proje);
        }
    }
}
