using DAL.Models;


namespace BLL.DTO
{
    public class QueueOrderRequest
    {
        public Order? Orders { get; set; }
        public int OrderId { get; set; }
    }
}
