using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITypeOfGoodService
    {
        IEnumerable<TypeOfGoodModel> GetAll();
        TypeOfGoodModel Get(int id);
        //IEnumerable<GoodService> Find(Func<T, Boolean> predicate);
        void Create(TypeOfGoodRequest item);
        void Update(TypeOfGoodRequest item, int id);
        void Delete(int id);
    }
}
