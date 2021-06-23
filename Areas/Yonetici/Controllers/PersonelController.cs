using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeYonetim.Data;
using ProjeYonetim.Models;
using ProjeYonetim.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ProjeYonetim.Areas.Yonetici.Controllers
{
    public class PersonelController : TemelController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

       
        public PersonelController(ApplicationDbContext context,IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _webHostEnvironment = hostingEnv;
        }

        public async Task<IActionResult> Index()
        {
            var personels = await _context.Personels.Include(x=>x.Departman).Include(x=>x.PersonelProjes).ThenInclude(y=>y.Proje).ToListAsync();
            return View(personels);
           
        }

        public async Task<IActionResult> Detay(int? id)
        {
            if (id==null)
            {
                return RedirectToAction(nameof(Index));
            }
            var personel = await _context.Personels.Include(w => w.YapilacaklarListesis).Include(x => x.Departman).
                Include(x => x.PersonelProjes).ThenInclude(y => y.Proje).FirstOrDefaultAsync(z=>z.ID==id);
            if (personel==null)
            {
                return NotFound();
            }
            return View(personel);
        }

        public IActionResult Ekle()
        {

            ViewBag.dep = new SelectList(_context.Departmans, nameof(Departman.ID), nameof(Departman.DepartmanAdi));
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(PersonelImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                    string uniqueFileName = UploadedFile(model);

                    var personel = new Personel
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        AdSoyad = model.Ad + " " + model.Soyad,
                        Cinsiyet = model.Cinsiyet,
                        DogumTarihi = model.DogumTarihi,
                        Email = model.Email,
                        TelefonNo = model.TelefonNo,
                        Maas = model.Maas,
                        ProfilResmi = uniqueFileName,
                        DeparmanID = model.DeparmanID,
                        
                    };
                    _context.Personels.Add(personel);
                    int sonuc=_context.SaveChanges();
                  
                    if (sonuc >= 1)
                    {
                       
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        throw new Exception("Kaydedilemedi!");
                
            }
            ViewBag.dep = new SelectList(_context.Departmans, nameof(Departman.ID), nameof(Departman.DepartmanAdi));
            
            return View();
        }

        public IActionResult Duzenle(PersonelImageViewModel per, int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            Personel personel = _context.Personels.FirstOrDefault(x => x.ID == id);
            
            PersonelImageViewModel person = new PersonelImageViewModel()
            {
                ID = personel.ID,
                Ad = personel.Ad,
                Soyad = personel.Soyad,

                Cinsiyet = personel.Cinsiyet,
                DogumTarihi = personel.DogumTarihi,
                Email = personel.Email,
                TelefonNo = personel.TelefonNo,
                Maas = personel.Maas,
               
                DeparmanID = personel.DeparmanID,
            };

            if (personel == null)
                return NotFound();
            ViewBag.dep = new SelectList(_context.Departmans, nameof(Departman.ID), nameof(Departman.DepartmanAdi));

            return View(person);
        }

        [HttpPost]
        public IActionResult Duzenle(PersonelImageViewModel model,int id)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                int sonuc = 0;
                Personel personel = _context.Personels.FirstOrDefault(x => x.ID == id);
                personel.Ad = model.Ad;
                personel.Soyad = model.Soyad;
                personel.AdSoyad = model.Ad + " " + model.Soyad;
                personel.Cinsiyet = model.Cinsiyet;
                personel.DogumTarihi = model.DogumTarihi;
                personel.Email = model.Email;
                personel.TelefonNo = model.TelefonNo;
                personel.Maas = model.Maas;
                if (model.ProfilImage==null)
                {
                     personel.ProfilResmi =personel.ProfilResmi;
                }
                else
                {
                    personel.ProfilResmi = uniqueFileName;
                }
                ViewBag.dep = new SelectList(_context.Departmans, nameof(Departman.ID), nameof(Departman.DepartmanAdi));
                personel.DeparmanID = model.DeparmanID;

                sonuc = _context.SaveChanges();

                if (sonuc>=1)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }

        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels
                .FirstOrDefaultAsync(m => m.ID == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(Personel person, int id)
        {
            int sonuc = 0;
           
                var personel = await _context.Personels.FindAsync(id);
                _context.Personels.Remove(personel);
                sonuc = await _context.SaveChangesAsync();
               
                if (sonuc >= 1)
                {
                   
                    return RedirectToAction(nameof(Index));
                }
            return View(person);
        }

        private string UploadedFile(PersonelImageViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfilImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
