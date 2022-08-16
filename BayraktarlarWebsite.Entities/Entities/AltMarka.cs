using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class AltMarka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Her alt markanın bir üst markası vardır
        public int MarkaId { get; set; }
        public  Marka Marka { get; set; }
    }
}
