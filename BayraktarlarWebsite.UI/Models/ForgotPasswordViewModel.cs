using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Lütfen e-mailinizi giriniz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
    }
}
