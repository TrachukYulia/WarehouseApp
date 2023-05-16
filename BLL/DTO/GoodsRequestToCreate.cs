

namespace BLL.DTO
{
    public class GoodsRequestToCreate
    {
        public int Amount { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int TypeOfGoodId { get; set; }
    }
}
