

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int GoodId { get; set; }
        public int CustomerId { get; set; }
        public int Amount { get; set; }
        public StatusOfOrder StatusOfOrder { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public ICollection<Queue>? Queue { get; set; }
        public Customer? Customer { get; set; }
        public Good? Goods { get; set; }
    }
}
