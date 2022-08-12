using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Required()]
        public string EMail { get; set; }
        [Required()]
        public string Token { get; set; }
        [Required(ErrorMessage ="Şifre alanı gereklidir")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="Şifre tekrar alanı gereklidir")]
        [DataType(DataType.Password)]
        [Compare(otherProperty:"NewPassword",ErrorMessage ="Girdiğiniz şifreler birbirleri ile uyuşmuyor")]
        public string NewPasswordConfirm { get; set; }
    }
}
