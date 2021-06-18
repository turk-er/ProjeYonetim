using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Models.ViewModels
{
    public class PersonelImageViewModel
    {
        public PersonelImageViewModel()
        {
            PersonelProjes = new List<PersonelProje>();
        }
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen Ad Soyad Bilgilerini Giriniz"),  DisplayName("Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen Ad Soyad Bilgilerini Giriniz"), DisplayName("Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Lütfen Doğum Tarihini Giriniz"), DisplayName("Doğum Tarihi :"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DogumTarihi { get; set; }

        
        [Required(ErrorMessage = "Lütfen Cinsiyet Bilgisini Giriniz"), DisplayName("Cinsiyet")]
        public string Cinsiyet { get; set; }

        [Required(ErrorMessage = "Telefon Numarasını Giriniz"), DisplayName("Telefon No"), MaxLength(15), DataType(DataType.PhoneNumber)]
        public string TelefonNo { get; set; }

        [Required(ErrorMessage = "Mail Adresini Giriniz"), DisplayName("Mail Adresi"), MaxLength(64), DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Maaş bilgilerini giriniz"), DisplayName("Maaş"), DataType(DataType.Currency)]
        public double Maas { get; set; }

  
      

        [ DisplayName("Profil Resmi")]
        public IFormFile ProfilImage { get; set; }

        public List<PersonelProje> PersonelProjes { get; set; }

        [ForeignKey("Departman")]
        public int DeparmanID { get; set; }

        public Departman Departman { get; set; }


        public List<YapilacaklarListesi> YapilacaklarListesis { get; set; }


    }
}
