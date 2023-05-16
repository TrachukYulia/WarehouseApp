using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Test.ServicesTests.Helper;

namespace Warehouse.Test.ServicesTests
{
    public class QueueServiceTests
    {
        private ServiceHelper? _data;

        [Theory]
        [InlineData(1)]
        public void QueueService_Get_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(x => x.QueueRepository.Get(id))
                .Returns(_data.QueuesTest.First(s => s.Id == id));
            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = queueService.Get(id);

            mockUnitOfWork.Verify(x => x.QueueRepository.Get(id), Times.Once);
            Assert.Equal(id, actual.Id);
            Assert.Equal(_data.QueuesTest.First(s => s.Id == id).OrderId, actual.OrderId);
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(100)]
        public void QueueService_Get_ShouldReturnNotFoundException(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Get(id));
            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => queueService.Get(id));
        }

        [Fact]
        public void QueueService_GetALl_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.GetAll())
                .Returns(_data.QueuesTest);

            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = queueService.GetAll().ToList();

            mockUnitOfWork.Verify(x => x.QueueRepository.GetAll(), Times.Once);
            Assert.Equal(actual.Count, _data.QueuesTest.Count);
            Assert.NotNull(actual);

        }
        [Fact]
        public void QueueService_Create_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Create(It.IsAny<Queue>()));
            var itemRequest = new QueueRequest { OrderId = 1 };

            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);
            queueService.Create(itemRequest);


            mockUnitOfWork.Verify(x => x.QueueRepository.Create(It.IsAny<Queue>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void QueueService_Create_ShouldReturnArgumentNullException()
        {
            _data = new ServiceHelper();
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.QueueRepository.Update(It.IsAny<Queue>()));
            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);
            QueueRequest itemRequest = null;

            Assert.Throws<ArgumentNullException>(() => queueService.Create(itemRequest));
        }
        [Fact]
        public void QueueService_Update_ShouldReturnCorrectValue()
        {
            int id = 1;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Update(It.IsAny<Queue>()));
            mockUnitOfWork.Setup(x => x.QueueRepository.Get(id))
            .Returns(_data.QueuesTest.First(s => s.Id == id));
            var itemRequest = new QueueRequest { OrderId = 1 };

            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);
            queueService.Update(itemRequest, id);

            mockUnitOfWork.Verify(x => x.QueueRepository.Get(id), Times.Once);
            mockUnitOfWork.Verify(x => x.QueueRepository.Update(It.IsAny<Queue>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void QueueService_Update_ShouldReturnNotFoundExeption()
        {
            int id = 100;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Update(It.IsAny<Queue>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.Get(id));
            var itemRequest = new QueueRequest { OrderId = 1 };
            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => queueService.Update(itemRequest, id));
        }

        [Theory]
        [InlineData(1)]
        public void QueueService_Delete_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Delete(id));
            mockUnitOfWork
            .Setup(x => x.QueueRepository.Get(id))
                .Returns(_data.QueuesTest.First(s => s.Id == id));

            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);
            queueService.Delete(id);

            mockUnitOfWork.Verify(x => x.QueueRepository.Delete(id), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public void QueueService_Delete_ShouldReturnNotFoundExeption(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.QueueRepository.Delete(id));

            var queueService = new QueueService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => queueService.Delete(id));
        }
    }
}
