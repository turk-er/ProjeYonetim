﻿using System;
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
    public class DepartmanController : TemelController
    {
        private readonly ApplicationDbContext _context;

        public DepartmanController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var departman = await _context.Departmans.ToListAsync();
            return View(departman);
        }

       
        public async Task<IActionResult> Detay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departmans.FirstOrDefaultAsync(m => m.ID == id);
            if (departman == null)
            {
                return NotFound();
            }

            return View(departman);
        }

       
        public IActionResult Ekle()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle([Bind("ID,DepartmanAdi")] Departman departman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departman);
        }

        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departmans.FindAsync(id);
            if (departman == null)
            {
                return NotFound();
            }
            return View(departman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, [Bind("ID,DepartmanAdi")] Departman departman)
        {
            if (id != departman.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmanExists(departman.ID))
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
            return View(departman);
        }

       
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departman = await _context.Departmans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (departman == null)
            {
                return NotFound();
            }

            return View(departman);
        }

    
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            var departman = await _context.Departmans.FindAsync(id);
            _context.Departmans.Remove(departman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmanExists(int id)
        {
            return _context.Departmans.Any(e => e.ID == id);
        }
    }
}
