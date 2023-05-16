using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGoodService
    {
        IEnumerable<GoodModel> GetAll();
        GoodModel Get(int id);
        void Create(GoodsRequestToCreate item);
        void Update(GoodsRequest item, int id);
        void Delete(int id);
    }
}
