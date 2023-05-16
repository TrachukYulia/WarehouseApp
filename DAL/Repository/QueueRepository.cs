using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;


namespace DAL.Repository
{
    public class QueueRepository: IRepository<Queue>
    {
        private WarehouseContext _warehouseContext;

        public QueueRepository(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public IEnumerable<Queue> GetAll()
        {
            return _warehouseContext.Queues
                .Include(o => o.Orders)
                .Include(o => o.Orders.Goods)
                .Include(o => o.Orders.Customer)
                .ToList();
        }

        public Queue Get(int id)
        {
            return _warehouseContext.Queues
                .Include(o => o.Orders)
                .Include(o => o.Orders.Goods)
                .Include(o => o.Orders.Customer)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Create(Queue queue)
        {
            _warehouseContext.Queues.Add(queue);
        }

        public void Update(Queue queue)
        {
            _warehouseContext.Entry(queue).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Queue queue = _warehouseContext.Queues.Find(id);
            if (queue != null)
                _warehouseContext.Queues.Remove(queue);
        }
    }
}
