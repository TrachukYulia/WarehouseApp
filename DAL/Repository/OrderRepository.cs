using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class OrderRepository: IRepository<Order>
    {
        private WarehouseContext _warehouseContext;

        public OrderRepository(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return _warehouseContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Goods)
                .Include(o => o.Queue)
                .ToList();
        }

        public Order Get(int id)
        {
            return _warehouseContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Goods)
                .Include(o => o.Queue)
                .First(o => o.Id == id);
        }

        public void Create(Order order)
        {
            _warehouseContext.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _warehouseContext.Entry(order).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Order order = _warehouseContext.Orders.Find(id);
            if (order != null)
                _warehouseContext.Orders.Remove(order);
        }
    }
}
