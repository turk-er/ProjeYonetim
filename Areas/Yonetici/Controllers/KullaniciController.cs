using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjeYonetim.Data;
using ProjeYonetim.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Areas.Yonetici.Controllers
{
    public class KullaniciController : TemelController
    {
        private  ApplicationDbContext _context;
        private readonly ILogger<KullaniciController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
       
      

        public KullaniciController(
            ApplicationDbContext context, 
            ILogger<KullaniciController> logger, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager
            
            
        )
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #region kullanici
        public IActionResult Index()
        {
            var usersWithRoles = _context.Users.ToList();
            return View(usersWithRoles);

        }
        public IActionResult Detay(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var kullanici = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if (kullanici==null)
            {
                return NotFound();
            }
            return View(kullanici);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(IdentityUser user)
        {
            try
            {
                await _userManager.CreateAsync(user);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Kullanici Ekleme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            return View(user);
        }

        public async Task<IActionResult> Duzenle(string id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Duzenle(string id, IdentityUser user)
        {
            try
            {
                await _userManager.UpdateAsync(user);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Kullanici Duzenleme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }

       
            return View(user);
        }

        public async Task<IActionResult> Sil(string id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Sil(string id, IdentityUser user)
        {
            try
            {
                IdentityUser _user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(_user);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Kullanici Silme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            return View(user);
        }
#endregion
        #region UserRole Islemleri

        public async Task<IActionResult> KullaniciyaRolVer(string id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            ViewData["Roller"] = new SelectList(_roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
            ViewData["Kullanici"] = user;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciyaRolVer(string id, List<string> rol)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            try
            {
                IdentityResult identityResult = await _userManager.AddToRolesAsync(user, rol);
                
                if (identityResult.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Kullanici Rol Verme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            ViewData["Roller"] = new SelectList(_roleManager.Roles, nameof(IdentityRole.Id), nameof(IdentityRole.Name));
            ViewData["Kullanici"] = user;
            return View(user);
        }

        #endregion

        #region Rol Islemleri

        public IActionResult RolListe()
        {
            List<IdentityRole> roller = _roleManager.Roles.ToList();
            return View(roller);
        }

        public IActionResult RolEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RolEkle(IdentityRole role)
        {
            try
            {
                await _roleManager.CreateAsync(role);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Rol Ekleme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            return View(role);
        }

        public async Task<IActionResult> RolDuzenle(string id)
        {
            if (id == null)
                return RedirectToAction(nameof(RolListe));

            IdentityRole rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
                return NotFound();

            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> RolDuzenle(string id, IdentityRole role)
        {
            try
            {
                IdentityRole rol = await _roleManager.FindByIdAsync(id);
                rol.Name = role.Name;
               
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Rol Duzenleme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            return View(role);
        }

        public async Task<IActionResult> RolSil(string id)
        {
            if (id == null)
                return RedirectToAction(nameof(RolListe));

            IdentityRole rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
                return NotFound();

            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> RolSil(string id, IdentityRole role)
        {
            try
            {
                IdentityRole rol = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Rol Silme islemi gerceklestirilemedi! - {Tarih}", DateTime.Now);
            }
            return View(role);
        }

        #endregion
    }
}
