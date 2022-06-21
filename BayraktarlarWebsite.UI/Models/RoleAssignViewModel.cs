using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class RoleAssignViewModel
    {
        public RoleAssignViewModel()
        {
            Roles = new List<Role>();
        }
        public User User { get; set; }
        public List<Role> Roles { get; set; }
    }
}
