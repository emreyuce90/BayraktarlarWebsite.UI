using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class ChangePasswordViewModel
    {
        [DisplayName("Eski Şifreniz :")]
        [Required(ErrorMessage ="Eski şifreniz alanı gereklidir")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DisplayName("Yeni Şifreniz :")]
        [Required(ErrorMessage = "Yeni şifreniz alanı gereklidir")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Yeni Şifreniz Tekrar :")]
        [Compare("NewPassword",ErrorMessage ="Girdiğiniz şifreler birbiri ile uyuşmuyor")]
        [Required(ErrorMessage = "Yeni şifreniz tekrar alanı gereklidir")]
        [DataType(DataType.Password)]
        public string NewPasswordAgain { get; set; }

    }
}
