namespace BayraktarlarWebsite.Entities.Entities
{
    public class HunterProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Hunter Hunter { get; set; }
        public int HunterId { get; set; }

    }
}
