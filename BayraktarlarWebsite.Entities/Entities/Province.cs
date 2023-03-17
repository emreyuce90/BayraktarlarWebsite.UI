using BayraktarlarWebsite.Shared.Interface;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class Town:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
