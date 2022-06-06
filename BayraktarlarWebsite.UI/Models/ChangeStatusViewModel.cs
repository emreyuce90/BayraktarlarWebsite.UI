using BayraktarlarWebsite.Entities.Entities;
using System.Collections.Generic;

namespace BayraktarlarWebsite.UI.Models
{
    public class ChangeStatusViewModel
    {
        public int TabelaId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public List<Status> Statuses { get; set; }
    }
}
