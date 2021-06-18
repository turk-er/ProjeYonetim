using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models
{
    public class YapilacaklarListesi
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Başlığı Giriniz"), MaxLength(50), Display(Name = "Görev Adı")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "İçeriği Giriniz Giriniz"), MaxLength(250), Display(Name = "Görev İçeriği"), DataType(DataType.MultilineText)]
        public string Gorev { get; set; }

        [Display(Name = "Görev Başlama Zamanı"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BaslamaZamani { get; set; }

        [Display(Name = "Görev Bitiş Zamanı"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BitisZamanı { get; set; }

        [Required, MaxLength(10), Display(Name = "Görev Önceliğini Giriniz"), DataType(DataType.Text)]
        public string Oncelik { get; set; }

        public string EkAlan { get; set; }

        [ForeignKey("Personel")]
        public int PersonelID { get; set; }

        public Personel Personel { get; set; }
    }
}
