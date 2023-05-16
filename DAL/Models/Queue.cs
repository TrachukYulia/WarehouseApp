

namespace DAL.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public Order? Orders { get; set; }
        public int OrderId { get; set; }
    }
}
