using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class UserWithRolesViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EMail { get; set; }
        public IList<RoleViewModel> Roles { get; set; }
    }
}
