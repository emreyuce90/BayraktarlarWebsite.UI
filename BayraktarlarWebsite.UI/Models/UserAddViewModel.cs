using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class UserAddViewModel
    {
        //Email adresi
        [DisplayName("E-Posta :")]
        [Required(ErrorMessage ="Email alanı zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //Şifresi
        [DataType(DataType.Password)]
        [DisplayName("Şifre :")]
        [Required(ErrorMessage = "Parola alanı zorunludur")]
        public string Password { get; set; }
        //Şifre tekrar
        [DataType(DataType.Password)]
        [DisplayName("Şifre tekrar :")]
        [Required(ErrorMessage = "Parola onay alanı zorunludur")]
        [Compare("Password",ErrorMessage ="Parolalar birbirleriyle eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        //b2b kodu
        [DisplayName("B2B Kodu :")]
        [Required(ErrorMessage = "B2B kod alanı girilmelidir")]
        public string Code { get; set; }
        //Cep numarası
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Cep Telefonu :")]
        public string Mobile { get; set; }
        public DateTime EntryDate { get; set; }
        //Profil Resmi Alanı
        [DisplayName("Fotoğraf :")]
        public string Picture { get; set; }
        //Profil Resmi Taşıma
        public IFormFile Profile { get; set; }

        [Required(ErrorMessage ="İsim alanı boş geçilemez")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        public string LastName { get; set; }
    }
}
