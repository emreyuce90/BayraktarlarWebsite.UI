using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class TabelaAddDto
    {
        public int BrandId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        public int MaterialId { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }

        public IList<TabelaImages> Images { get; set; }

    }
}
