using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models
{
    
    public class PersonelProje
    {
        [Key]
        public  int ID { get; set; }

        [ForeignKey("Personel")]
        public int PersonelID { get; set; }

        [ForeignKey("Proje")]
        public int ProjeID { get; set; }

        public virtual Personel Personel { get; set; }

        public virtual Proje Proje { get; set; }
    }
}
