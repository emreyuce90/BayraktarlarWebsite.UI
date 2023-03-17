using BayraktarlarWebsite.Shared.Interface;

namespace BayraktarlarWebsite.Entities.Entities
{
    public class District:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TownId { get; set; }
    }
}
