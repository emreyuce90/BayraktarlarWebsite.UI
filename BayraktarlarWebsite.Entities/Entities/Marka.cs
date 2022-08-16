using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Marka
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<AltMarka> AltMarkalar { get; set; }
    }
}
