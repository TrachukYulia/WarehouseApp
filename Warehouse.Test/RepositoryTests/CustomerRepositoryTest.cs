using DAL.Models;
using DAL.Repository;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Warehouse.Test.RepositoryTests
{
    public class CustomerRepositoryTest
    {
        private readonly List<Customer> expectedCustomer = new List<Customer>
        { 
            new Customer { Id = 1, Name = "Anna", Surname = "Hatuwey", PhoneNumber = "+380777666"},
            new Customer { Id = 2,  Name = "Sophie", Surname = "Richi", PhoneNumber = "+380666777"},
            new Customer { Id = 3,  Name = "For delete", Surname = "For delete", PhoneNumber = "+380000000"}
        };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CustomerRepository_Get_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var customerRepository = new CustomerRepository(context);
            var expected = expectedCustomer.FirstOrDefault(x => x.Id == id);

            var actual = customerRepository.Get(id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Surname, actual.Surname);
            Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        }
        [Fact]
        public void GetCustomerRepository_Getter_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            UnitOfWork test = new UnitOfWork(context);

            _ = Assert.IsAssignableFrom<CustomerRepository>(test.CustomerRepository);
        }

        [Fact]
        public void CustomerRepository_GetAll_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var customerRepository = new CustomerRepository(context);
            var expected = expectedCustomer;

            var actual = customerRepository.GetAll().ToList();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void CustomerRepository_Update_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var customerRepository = new CustomerRepository(context);
            var customer = new Customer { Id = 1, Name = "New Name", Surname = "New Surname", PhoneNumber = "+380999777" };
            var expected = customer;

            customerRepository.Update(customer);
            var actual = context.Customers.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Surname, actual.Surname);
            Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        }

        [Theory]
        [InlineData(3)]
        public void CustomerRepository_Delete_ShouldReturnInCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var customerRepository = new CustomerRepository(context);

            customerRepository.Delete(id);
            context.SaveChanges();
            var actual = context.Customers.Contains(context.Customers.FirstOrDefault(x => x.Id == id));

            Assert.False(actual);
        }

        [Fact]
        public void CustomerRepository_Create_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var customerRepository = new CustomerRepository(context);
            var itemToAdd = new Customer
            {
                Name = "For add",
                Surname = "For add",
                PhoneNumber = "+380000000"
            };

            customerRepository.Create(itemToAdd);
            context.SaveChanges();
            var actual = context.Customers.Contains(itemToAdd);

            Assert.True(actual);
        }

    }
}
