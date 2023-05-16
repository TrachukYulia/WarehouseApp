using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService
    {
        public IGoodService GoodService { get; }

        public ITypeOfGoodService TypeOfGoodService { get; }

        public IOrderService OrderService { get; }
        public ICustomerService CustomerService { get; }
        public IQueueService QueueService { get; }


    }
}
