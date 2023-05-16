
namespace DAL.Models
{
    public class Customer
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
