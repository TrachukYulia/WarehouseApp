using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;


namespace DAL.Repository
{
    public class CustomerRepository: IRepository<Customer>
    {
        private WarehouseContext _warehouseContext;

        public CustomerRepository(WarehouseContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _warehouseContext.Customers
                .Include(o => o.Orders)
                .ToList();
        }

        public Customer Get(int id)
        {
            return _warehouseContext.Customers
                .Include(o => o.Orders)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Create(Customer customer)
        {
            _warehouseContext.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _warehouseContext.Entry(customer).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Customer customer = _warehouseContext.Customers.Find(id);
            if (customer != null)
                _warehouseContext.Customers.Remove(customer);
        }
    }
}
