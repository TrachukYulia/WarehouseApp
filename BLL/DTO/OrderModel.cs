

namespace BLL.DTO
{
    public class OrderModel
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int Amount { get; set; }
        public string? StatusOfOrder { get; set; }
        public DateTime TimeCreated { get; set; }
        public string? Customer { get; set; }

        public string? Goods { get; set; }
    }
}
