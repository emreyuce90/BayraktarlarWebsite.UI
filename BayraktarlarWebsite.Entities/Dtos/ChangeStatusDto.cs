using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class ChangeStatusDto
    {
        public int TabelaId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public StatusListDto Statuses { get; set; }
    }
}
