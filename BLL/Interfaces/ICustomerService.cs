using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAll();
        CustomerModel Get(int id);
        void Create(CustomerRequest item);
        void Update(CustomerRequest item, int id);
        void Delete(int id);
    }
}
