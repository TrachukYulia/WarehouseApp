using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Test.ServicesTests.Helper;
using Moq;
using DAL.Models;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;
using BLL.Exceptions;

namespace Warehouse.Test.ServicesTests
{
    public class TypeOfGoodServiceTests
    {
        private ServiceHelper _data;

        [Theory]
        [InlineData(1)]
        public void TypeOfGoodService_Get_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Get(id))
                .Returns(_data.TypeOfGoodsTest.First(s => s.Id == id));
            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());

            var actual = typeOfGoodService.Get(id);

            Assert.NotNull(actual);
            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.Get(id), Times.Once);
            Assert.Equal(actual.Id, id);
            Assert.Equal(actual.Name, _data.TypeOfGoodsTest.First(s => s.Id == id).Name);
        }

        [Theory]
        [InlineData(100)]
        public void TypeOfGoodService_Get_ShouldReturnNotFoundException(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Get(id));
            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());

            Assert.Throws<NotFoundException>(() => typeOfGoodService.Get(id));
        }

        [Fact]
        public void TypeOfGoodService_GetALl_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.GetAll())
                .Returns(_data.TypeOfGoodsTest);

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());

            var actual = typeOfGoodService.GetAll().ToList();

            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.GetAll(), Times.Once);
            Assert.Equal(actual.Count, _data.TypeOfGoodsTest.Count);
        }
        [Fact]
        public void TypeOfGoodService_Create_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Create(It.IsAny<TypeOfGood>()));
            var itemRequest = new TypeOfGoodRequest { Name = "New type" };

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());
            typeOfGoodService.Create(itemRequest);


            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.Create(It.IsAny<TypeOfGood>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void TypeOfGoodService_Update_ShouldReturnCorrectValue()
        {
            int id = 1;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Update(It.IsAny<TypeOfGood>()));
                mockUnitOfWork.Setup(x => x.TypeOfGoodRepository.Get(id))
                .Returns(_data.TypeOfGoodsTest.First(s => s.Id == id)); 
            var itemRequest = new TypeOfGoodRequest { Name = "New type" };

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());
            typeOfGoodService.Update(itemRequest, id);

            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.Get(id), Times.Once);
            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.Update(It.IsAny<TypeOfGood>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void TypeOfGoodService_Update_ShouldReturnNotFoundExeption()
        {
            int id = 100;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Update(It.IsAny<TypeOfGood>()));
            mockUnitOfWork.Setup(x => x.TypeOfGoodRepository.Get(id));
            var itemRequest = new TypeOfGoodRequest { Name = "New type" };

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());

            Assert.Throws<NotFoundException>(() => typeOfGoodService.Update(itemRequest, id));
        }

        [Theory]
        [InlineData(2)]
        public void TypeOfGoodService_Delete_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Delete(id));
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Get(id))
                .Returns(_data.TypeOfGoodsTest.First(s => s.Id == id));

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());
            typeOfGoodService.Delete(id);

            mockUnitOfWork.Verify(x => x.TypeOfGoodRepository.Delete(id), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public void TypeOfGoodService_Delete_ShouldReturnNotFoundExeption(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TypeOfGoodRepository.Delete(id));

            ITypeOfGoodService typeOfGoodService = new TypeOfGoodService(mockUnitOfWork.Object, _data.CreateMapperProfile());

            Assert.Throws<NotFoundException>(() => typeOfGoodService.Delete(id));
        }


    }
}
