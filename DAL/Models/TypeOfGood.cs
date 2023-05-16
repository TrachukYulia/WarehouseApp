

namespace DAL.Models
{
    public class TypeOfGood
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Good>? Goods { get; set; }
    }
}
