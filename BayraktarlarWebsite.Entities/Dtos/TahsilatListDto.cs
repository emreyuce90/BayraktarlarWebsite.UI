using BayraktarlarWebsite.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Dtos
{
    public class TahsilatListDto
    {
        public IList<Tahsilat> Tahsilats { get; set; }
        public float SumTotal { get; set; }

    }
}
