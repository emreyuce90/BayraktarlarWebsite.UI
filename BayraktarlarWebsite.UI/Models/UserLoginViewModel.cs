using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="B2B kodu alanı gereklidir")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Şifre alanı gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
