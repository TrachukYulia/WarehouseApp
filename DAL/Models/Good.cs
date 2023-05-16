

namespace DAL.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public int TypeOfGoodId { get; set; }
        public decimal Price { get; set; }
        public TypeOfGood TypeOfGood { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
