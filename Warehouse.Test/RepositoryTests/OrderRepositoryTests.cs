using DAL.Models;
using DAL.Repository;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace Warehouse.Test.RepositoryTests
{
    public class OrderRepositoryTests
    {
        private readonly List<Order> expectedOrder = new List<Order>
        {
                new Order {Id = 1, GoodId = 1, CustomerId = 1, Amount = 5, TotalPrice = 250,  StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now},
                new Order {Id = 2, GoodId = 2, CustomerId = 2, Amount = 20, TotalPrice = 0,  StatusOfOrder = StatusOfOrder.Active, TimeCreated = DateTime.Now},
                new Order {Id = 3, GoodId = 3, CustomerId = 2, Amount = 40, TotalPrice = 0,  StatusOfOrder = StatusOfOrder.Active, TimeCreated = DateTime.Now}
        };


        [Fact]
        public void GetOrderRepository_Getter_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            UnitOfWork test = new UnitOfWork(context);
            
            _ = Assert.IsAssignableFrom<OrderRepository>(test.OrderRepository);          
        }
    
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void OrderRepository_Get_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var orderRepository = new OrderRepository(context);
            var expected = expectedOrder.FirstOrDefault(x => x.Id == id);

            var actual = orderRepository.Get(id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.GoodId, actual.GoodId);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.TotalPrice, actual.TotalPrice);
            Assert.Equal(expected.StatusOfOrder, actual.StatusOfOrder);

        }

        [Fact]
        public void OrderRepository_GetAll_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var orderRepository = new OrderRepository(context);
            var expected = expectedOrder;

            var actual = orderRepository.GetAll().ToList();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Theory]
        [InlineData(1)]
        public void OrderRepository_Update_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var orderRepository = new OrderRepository(context);
            var order = new Order { Id = id, GoodId = 1, CustomerId = 2, Amount = 1, TotalPrice = 50, StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now };
            var expected = order;

            orderRepository.Update(order);
            var actual = context.Orders.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.GoodId, actual.GoodId);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.TotalPrice, actual.TotalPrice);
            Assert.Equal(expected.StatusOfOrder, actual.StatusOfOrder);
        }

        [Theory]
        [InlineData(3)]
        public void OrderRepository_Delete_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var orderRepository = new OrderRepository(context);

            orderRepository.Delete(id);
            context.SaveChanges();
            var actual = context.Orders.Contains(context.Orders.FirstOrDefault(x => x.Id == id));

            Assert.False(actual);
        }

        [Fact]
        public void OrderRepository_Create_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var orderRepository = new OrderRepository(context);
            var itemToAdd = new Order
            {
                GoodId = 2,
                CustomerId = 2,
                Amount = 100,
                TotalPrice = 0,
                StatusOfOrder = StatusOfOrder.Active,
                TimeCreated = DateTime.Now
            };

            orderRepository.Create(itemToAdd);
            context.SaveChanges();
            var actual = context.Orders.Contains(itemToAdd);

            Assert.True(actual);
        }

    }
}

