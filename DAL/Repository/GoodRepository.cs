using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repository
{
    public class GoodRepository: IRepository<Good>
    {
        private WarehouseContext _warehouseContext;

        public GoodRepository(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public IEnumerable<Good> GetAll()
        {
            return _warehouseContext.Goods
                .Include(o => o.TypeOfGood)
                .ToList();
        }

        public Good Get(int id)
        {
            return _warehouseContext.Goods
                .Include(o => o.TypeOfGood)
                .FirstOrDefault(o => o.Id == id);
            
        }

        public void Create(Good good)
        {
            _warehouseContext.Goods.Add(good);
        }

        public void Update(Good good)
        {
            _warehouseContext.Entry(good).State = EntityState.Modified;
        }
  
        public void Delete(int id)
        {
            Good good = _warehouseContext.Goods.Find(id);
            if (good != null)
                _warehouseContext.Goods.Remove(good);
        }
    }
}
