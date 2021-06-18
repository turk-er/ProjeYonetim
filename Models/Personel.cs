using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models
{
    public class Personel
    {
        public Personel()
        {
           YapilacaklarListesis = new List<YapilacaklarListesi>();

           PersonelProjes = new List<PersonelProje>();


           AdSoyad = Ad + " " + Soyad;
        }

        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "Lütfen Ad Soyad Bilgilerini Giriniz"), MaxLength(50), DisplayName("Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen Ad Soyad Bilgilerini Giriniz"), MaxLength(50), DisplayName("Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Lütfen Doğum Tarihini Giriniz"), DisplayName("Doğum Tarihi"),DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DogumTarihi { get; set; }

        [MaxLength(100), DisplayName("Ad Soyad")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "Lütfen Cinsiyet Bilgisini Giriniz"), DisplayName("Cinsiyet"), MaxLength(10)]
        public string Cinsiyet { get; set; }

        [Required(ErrorMessage = "Telefon Numarasını Giriniz"), DisplayName("Telefon No"), MaxLength(15), DataType(DataType.PhoneNumber)]
        public string TelefonNo { get; set; }

        [Required(ErrorMessage = "Mail Adresini Giriniz"), DisplayName("Mail Adresi"), MaxLength(64), DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Maaş bilgilerini giriniz"), DisplayName("Maaş"), DataType(DataType.Currency)]
        public double Maas { get; set; }

        [MaxLength(15)]
        public string EkAlan { get; set; }

        [Required(ErrorMessage = "Lütfen Profil Resmini Yükleyin"), DisplayName("Profil Resmi"), MaxLength(100)]
        public string ProfilResmi { get; set; }

       

        public List<PersonelProje> PersonelProjes{ get; set; }

        [ForeignKey("Departman")]
        public int DeparmanID { get; set; }

        public Departman Departman { get; set; }

        public List<YapilacaklarListesi> YapilacaklarListesis { get; set; }

     

        


    }
}
