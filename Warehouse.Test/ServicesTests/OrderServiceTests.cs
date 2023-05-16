using BLL.DTO;
using BLL.Exceptions;
using BLL.Services;
using DAL.Interfaces;
using Moq;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Test.ServicesTests.Helper;

namespace Warehouse.Test.ServicesTests
{
    public class OrderServiceTests
    {
        private ServiceHelper? _data;

        [Theory]
        [InlineData(1)]
        public void OrderService_Get_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(x => x.OrderRepository.Get(id))
                .Returns(_data.OrdersTest.First(s => s.Id == id));
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = orderService.Get(id);

            mockUnitOfWork.Verify(x => x.OrderRepository.Get(id), Times.Once);
            Assert.Equal(id, actual.Id);
            Assert.Equal(_data.OrdersTest.First(s => s.Id == id).Amount, actual.Amount);
            Assert.Equal(_data.OrdersTest.First(s => s.Id == id).TotalPrice, actual.TotalPrice);
            Assert.Equal(_data.OrdersTest.First(s => s.Id == id).StatusOfOrder.ToString(), actual.StatusOfOrder);
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(100)]
        public void OrderService_Get_ShouldReturnNotFoundException(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Get(id));
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => orderService.Get(id));
        }

        [Fact]
        public void OrderService_GetALl_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.GetAll())
                .Returns(_data.OrdersTest);

            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = orderService.GetAll().ToList();

            mockUnitOfWork.Verify(x => x.OrderRepository.GetAll(), Times.Once);
            Assert.Equal(actual.Count, _data.OrdersTest.Count);
            Assert.NotNull(actual);

        }
        [Fact]
        public void OrderService_GetALl_ShouldReturnNotFoundException()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.GetAll())
                .Returns((ICollection<Order>)null);
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);


            Assert.Throws<NotFoundException>(() => orderService.GetAll());

        }
        [Fact]
        public void OrderService_Create_DoneOrder_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Create(It.IsAny<Order>()));
            var itemRequest = new OrderRequest {GoodId = 1, CustomerId = 1, Amount = 5};
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);
            mockUnitOfWork
              .Setup(x => x.GoodRepository.Get(itemRequest.GoodId))
              .Returns(_data.GoodsTest.First(x => x.Id == itemRequest.GoodId));
            mockUnitOfWork
              .Setup(x => x.CustomerRepository.Get(itemRequest.CustomerId))
              .Returns(_data.CustomersTest.First(x => x.Id == itemRequest.CustomerId));

            orderService.Create(itemRequest);

            mockUnitOfWork.Verify(x => x.GoodRepository.Get(itemRequest.GoodId), Times.Once);
            mockUnitOfWork.Verify(x => x.CustomerRepository.Get(itemRequest.CustomerId), Times.Once);
            mockUnitOfWork.Verify(x => x.OrderRepository.Create(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void OrderService_Create_ActiveOrder_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Create(It.IsAny<Order>()));
            var itemRequest = new OrderRequest { GoodId = 1, CustomerId = 1, Amount = 200 };
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);
            mockUnitOfWork
              .Setup(x => x.GoodRepository.Get(itemRequest.GoodId))
              .Returns(_data.GoodsTest.First(x => x.Id == itemRequest.GoodId));
            mockUnitOfWork
              .Setup(x => x.CustomerRepository.Get(itemRequest.CustomerId))
              .Returns(_data.CustomersTest.First(x => x.Id == itemRequest.CustomerId));
            mockUnitOfWork
                .Setup(x => x.QueueRepository.GetAll())
                .Returns(_data.QueuesTest);

            orderService.Create(itemRequest);

            mockUnitOfWork.Verify(x => x.GoodRepository.Get(itemRequest.GoodId), Times.Once);
            mockUnitOfWork.Verify(x => x.CustomerRepository.Get(itemRequest.CustomerId), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void OrderService_Create_CustomerIsNull_ShouldReturnNotFoundException()
         {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Create(It.IsAny<Order>()));
            var itemRequest = new OrderRequest { GoodId = 1, CustomerId = 1, Amount = 2 };
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            mockUnitOfWork
             .Setup(x => x.GoodRepository.Get(itemRequest.GoodId))
             .Returns(_data.GoodsTest.First(x => x.Id == itemRequest.GoodId));
            mockUnitOfWork
              .Setup(x => x.CustomerRepository.Get(itemRequest.CustomerId))
              .Returns((Customer)null);
            Assert.Throws<NotFoundException>(() => orderService.Create(itemRequest));
        }
        [Fact]
        public void OrderService_Create_GoodIsNull_ShouldReturnNotFoundException()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Create(It.IsAny<Order>()));
            var itemRequest = new OrderRequest { GoodId = 1, CustomerId = 1, Amount = 2 };
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            mockUnitOfWork
             .Setup(x => x.GoodRepository.Get(itemRequest.GoodId))
             .Returns((Good)null);
            mockUnitOfWork
              .Setup(x => x.CustomerRepository.Get(itemRequest.CustomerId))
              .Returns(_data.CustomersTest.First(x => x.Id == itemRequest.CustomerId));
            Assert.Throws<NotFoundException>(() => orderService.Create(itemRequest));
        }


        [Fact]
        public void OrderService_Create_ShouldReturnArgumentNullException()
        {
            _data = new ServiceHelper();
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.OrderRepository.Update(It.IsAny<Order>()));
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);
            OrderRequest itemRequest = null;

            Assert.Throws<ArgumentNullException>(() => orderService.Create(itemRequest));
        }

        [Fact]
        public void OrderService_Update_ShouldReturnCorrectValue()
        {
            int id = 2;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Update(It.IsAny<Order>()));
            mockUnitOfWork.Setup(x => x.OrderRepository.Get(id))
            .Returns(_data.OrdersTest.First(s => s.Id == id));
            var itemRequest = new OrderGoodRequest { Amount = 30 };
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            mockUnitOfWork
             .Setup(x => x.GoodRepository.Get(id))
             .Returns(_data.GoodsTest.First(x => x.Id == id));

            mockUnitOfWork
                .Setup(x => x.QueueRepository.GetAll())
                .Returns(_data.QueuesTest);


            orderService.Update(itemRequest, id);

            mockUnitOfWork.Verify(x => x.OrderRepository.Get(id), Times.Once);
            mockUnitOfWork.Verify(x => x.OrderRepository.Update(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void OrderService_Update_ShouldReturnNotFoundExeption()
        {
            int id = 100;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Update(It.IsAny<Order>()));
            mockUnitOfWork.Setup(x => x.OrderRepository.Get(id));
            var itemRequest = new OrderGoodRequest { Amount = 10 };

            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => orderService.Update(itemRequest, id));
        }
        [Fact]
        public void OrderService_Update_ShouldReturnArgumentNullException()
        {
            int id = 1;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Update(It.IsAny<Order>()));
            mockUnitOfWork.Setup(x => x.OrderRepository.Get(id))
                .Returns(_data.OrdersTest.First(s => s.Id == id));
            OrderGoodRequest itemRequest = null;
            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<ArgumentNullException>(() => orderService.Update(itemRequest, id));
        }

        [Fact]
        public void OrderService_Update_ShouldReturnArgumentException()
        {
            int id = 1;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Update(It.IsAny<Order>()));
            mockUnitOfWork.Setup(x => x.OrderRepository.Get(id))
                .Returns(_data.OrdersTest.First(s => s.Id == id));
            var itemRequest = new OrderGoodRequest { Amount = 10 };

            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<ArgumentException>(() => orderService.Update(itemRequest, id));
        }


        [Theory]
        [InlineData(2)]
        public void OrderService_Delete_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Delete(id));
            mockUnitOfWork
            .Setup(x => x.OrderRepository.Get(id))
                .Returns(_data.OrdersTest.First(s => s.Id == id));

            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);
            orderService.Delete(id);

            mockUnitOfWork.Verify(x => x.OrderRepository.Delete(id), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public void OrderService_Delete_ShouldReturnNotFoundExeption(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.OrderRepository.Delete(id));

            var orderService = new OrderService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => orderService.Delete(id));
        }
    }
}
