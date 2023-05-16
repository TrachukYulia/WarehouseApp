using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class TypeOfGoodRepository: IRepository<TypeOfGood>
    {
        private WarehouseContext _warehouseContext;

        public TypeOfGoodRepository(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public IEnumerable<TypeOfGood> GetAll()
        {
            return _warehouseContext.TypeOfGoods
                .Include(o => o.Goods)
                .ToList();
        }

        public TypeOfGood Get(int id)
        {
            return _warehouseContext.TypeOfGoods
                .Include(o => o.Goods)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Create(TypeOfGood typeOfGood)
        {
            _warehouseContext.TypeOfGoods.Add(typeOfGood);
        }

        public void Update(TypeOfGood typeOfGood)
        {
            _warehouseContext.Entry(typeOfGood).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            TypeOfGood typeOfGood = _warehouseContext.TypeOfGoods.Find(id);
            if (typeOfGood != null)
                _warehouseContext.TypeOfGoods.Remove(typeOfGood);
        }
    }
}
