using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IQueueService
    {
        IEnumerable<QueueModel> GetAll();
        QueueModel Get(int id);
        void Create(QueueRequest item);
        void Update(QueueRequest item, int id);
        void Delete(int id);
    }
}
