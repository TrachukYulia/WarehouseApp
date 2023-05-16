using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;


namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private WarehouseContext _warehouseContext;
        private GoodRepository _goodRepository;
        private OrderRepository _orderRepository;
        private CustomerRepository _customerRepository;
        private QueueRepository _queueRepository;
        private TypeOfGoodRepository _typeOfGoodRepository;

        public UnitOfWork(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }
        public IRepository<Good> GoodRepository
        {
            get
            {
                if (_goodRepository == null)
                    _goodRepository = new GoodRepository(_warehouseContext);
                return _goodRepository;
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_warehouseContext);
                return _orderRepository;
            }
        }
        public IRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_warehouseContext);
                return _customerRepository;
            }
        }
        public IRepository<Queue> QueueRepository
        {
            get
            {
                if (_queueRepository == null)
                    _queueRepository = new QueueRepository(_warehouseContext);
                return _queueRepository;
            }
        }
        public IRepository<TypeOfGood> TypeOfGoodRepository
        {
            get
            {
                if (_typeOfGoodRepository == null)
                    _typeOfGoodRepository = new TypeOfGoodRepository(_warehouseContext);
                return _typeOfGoodRepository;
            }
        }

        public void Save()
        {
            _warehouseContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _warehouseContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
