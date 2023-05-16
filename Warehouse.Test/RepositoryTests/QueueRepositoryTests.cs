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
    public class QueueRepositoryTests
    {
        private readonly List<Queue> expectedQueue = new List<Queue>
        {
              new Queue { Id = 1, OrderId = 2},
              new Queue { Id = 2, OrderId = 3}
        };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void QueueRepository_Get_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var queueRepository = new QueueRepository(context);
            var expected = expectedQueue.FirstOrDefault(x => x.Id == id);

            var actual = queueRepository.Get(id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.OrderId, actual.OrderId);
        }

        [Fact]
        public void GetQueueRepository_Getter_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            UnitOfWork test = new UnitOfWork(context);

            _ = Assert.IsAssignableFrom<QueueRepository>(test.QueueRepository);
        }

        [Fact]
        public void QueueRepository_GetAll_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var queueRepository = new QueueRepository(context);
            var expected = expectedQueue;

            var actual = queueRepository.GetAll().ToList();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Theory]
        [InlineData(2)]
        public void QueueRepository_Update_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var queueRepository = new QueueRepository(context);
            var queue = new Queue { Id = id, OrderId = 3 };
            var expected = queue;

            queueRepository.Update(queue);
            var actual = context.Queues.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.OrderId, actual.OrderId);
        }

        [Theory]
        [InlineData(2)]
        public void QueueRepository_Delete_ShouldReturnInCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var queueRepository = new QueueRepository(context);

            queueRepository.Delete(id);
            context.SaveChanges();
            var actual = context.Queues.Contains(context.Queues.FirstOrDefault(x => x.Id == id));

            Assert.False(actual);
        }

        [Fact]
        public void QueueRepository_Create_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var queueRepository = new QueueRepository(context);
            var itemToAdd = new Queue
            {
                OrderId = 3
            };

            queueRepository.Create(itemToAdd);
            context.SaveChanges();
            var actual = context.Queues.Contains(itemToAdd);

            Assert.True(actual);
        }
    }
}
