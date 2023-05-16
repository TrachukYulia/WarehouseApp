using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Test.ServicesTests.Helper;

namespace Warehouse.Test.ServicesTests
{
    public class GoodServiceTests
    {
        private ServiceHelper _data;

        [Theory]
        [InlineData(1)]
        public void GoodService_Get_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Get(id))
                .Returns(_data.GoodsTest.First(s => s.Id == id));
            GoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = goodService.Get(id);

            Assert.NotNull(actual);
            mockUnitOfWork.Verify(x => x.GoodRepository.Get(id), Times.Once);
            Assert.Equal(id, actual.Id);
            Assert.Equal(_data.GoodsTest.First(s => s.Id == id).Name, actual.Name);
            Assert.Equal(_data.GoodsTest.First(s => s.Id == id).Amount, actual.Amount);

        }

        [Theory]
        [InlineData(100)]
        public void GoodService_Get_ShouldReturnNotFoundException(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Get(id));
            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => goodService.Get(id));
        }

        [Fact]
        public void GoodService_GetALl_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.GetAll())
                .Returns(_data.GoodsTest);

            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = goodService.GetAll().ToList();

            mockUnitOfWork.Verify(x => x.GoodRepository.GetAll(), Times.Once);
            Assert.Equal(actual.Count, _data.GoodsTest.Count);
        }
        [Fact]
        public void GoodService_GetAll_ShouldReturnNotFoundException()
        {

            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.GetAll())
                .Returns((ICollection<Good>)null);
            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => goodService.GetAll());
        }
        [Fact]
        public void GoodService_Create_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Create(It.IsAny<Good>()));
            var itemRequest = new GoodsRequestToCreate {Amount = 10, Price = 10, TypeOfGoodId = 1, Name = "New name"};

            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);
            goodService.Create(itemRequest);


            mockUnitOfWork.Verify(x => x.GoodRepository.Create(It.IsAny<Good>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void GoodService_Update_ShouldReturnCorrectValue()
        {
            int id = 2;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Update(It.IsAny<Good>()));
            mockUnitOfWork.Setup(x => x.GoodRepository.Get(id))
            .Returns(_data.GoodsTest.First(s => s.Id == id));
            var itemRequest = new GoodsRequest { Amount = 20 };
            GoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);
            mockUnitOfWork
                .Setup(x => x.QueueRepository.GetAll())
                .Returns(_data.QueuesTest);
            mockUnitOfWork
                  .Setup(x => x.OrderRepository.Get(id))
                  .Returns(_data.OrdersTest.First(s => s.Id == id));

            goodService.Update(itemRequest, id);

            mockUnitOfWork.Verify(x => x.GoodRepository.Get(id), Times.AtLeastOnce);
            mockUnitOfWork.Verify(x => x.OrderRepository.Get(id), Times.AtLeastOnce);
            mockUnitOfWork.Verify(x => x.QueueRepository.GetAll(), Times.AtLeastOnce);
            mockUnitOfWork.Verify(x => x.GoodRepository.Update(It.IsAny<Good>()), Times.AtLeastOnce);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void GoodService_Update_ShouldReturnNotFoundExeption()
        {
            int id = 100;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Update(It.IsAny<Good>()));
            mockUnitOfWork.Setup(x => x.GoodRepository.Get(id));
            var itemRequest = new GoodsRequest { Amount = 11 };

            GoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => goodService.Update(itemRequest, id));
        }

        [Theory]
        [InlineData(2)]
        public void GoodService_Delete_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.GoodRepository.Delete(id));
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Get(id))
                .Returns(_data.GoodsTest.First(s => s.Id == id));

            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);
            goodService.Delete(id);

            mockUnitOfWork.Verify(x => x.GoodRepository.Delete(id), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public void GoodService_Delete_ShouldReturnNotFoundExeption(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.GoodRepository.Delete(id));

            IGoodService goodService = new GoodService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => goodService.Delete(id));
        }

    }
}
