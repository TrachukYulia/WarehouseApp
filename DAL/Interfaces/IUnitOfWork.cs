using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Good> GoodRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Queue> QueueRepository { get; }
        IRepository<TypeOfGood> TypeOfGoodRepository { get; }
        void Save();
    }
}
