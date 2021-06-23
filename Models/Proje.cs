using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models
{
    public class Proje
    {
        public Proje()
        {
            PersonelProjes = new List<PersonelProje>();

            
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen Proje Adını Giriniz"), MaxLength(100), DisplayName("Proje Adı")]
        public string ProjeAdi { get; set; }

        [Required(ErrorMessage = "Proje Başlangıç Tarihini Giriniz"), DisplayName("Proje Başlangıç Tarihi"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BasTarihi { get; set; }

        [Required(ErrorMessage = "Proje Bitiş Tarihini Giriniz"), DisplayName("Proje Bitiş Tarihi"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BitTarihi { get; set; }


        [Required(ErrorMessage = "Proje Gelirini Giriniz"), DisplayName("Proje Ücreti"), DataType(DataType.Currency)]
        public double ProjeGeliri { get; set; }

        [DisplayName("Net Gelir"), DataType(DataType.Currency)]
        [NotMapped]
        public double? NetKar { get; set; }

        public string EkAlan { get; set; }

        public double?  Ay { get; set; }

        public List<PersonelProje> PersonelProjes { get; set; }

        [NotMapped]
        public virtual List<int> PersonelList { get; set; }


    }
}
