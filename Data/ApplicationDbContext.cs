using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjeYonetim.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Proje> Projes { get; set; }
        public DbSet<PersonelProje> PersonelProjes { get; set; }
        public DbSet<Departman> Departmans { get; set; }
        public DbSet<YapilacaklarListesi> YapilacaklarListesis { get; set; }

       

    
    }
}
