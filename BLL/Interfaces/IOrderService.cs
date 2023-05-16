using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderModel> GetAll();
        OrderModel Get(int id);
        void Create(OrderRequest item);
        void Update(OrderGoodRequest item, int id);
        void Delete(int id);
    }
}
