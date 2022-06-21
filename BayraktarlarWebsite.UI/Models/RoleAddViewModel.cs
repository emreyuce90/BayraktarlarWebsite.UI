using System.ComponentModel.DataAnnotations;

namespace BayraktarlarWebsite.UI.Models
{
    public class RoleAddViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
