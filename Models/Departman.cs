using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models
{
    public class Departman
    {
        public Departman()
        {
            Personels = new List<Personel>();
        }
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="DepartmanAdını Giriniz") ,MaxLength(30),DisplayName("Departman Adı")]
        public string DepartmanAdi { get; set; }

        public virtual List<Personel> Personels { get; set; }
    }
}
