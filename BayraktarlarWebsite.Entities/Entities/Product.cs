using System.Collections.Generic;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HunterProduct> HunterProducts{ get; set; }
        public List<SubProduct> SubProducts { get; set; }
    }
}
