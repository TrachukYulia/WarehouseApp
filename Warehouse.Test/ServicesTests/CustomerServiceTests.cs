using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using Moq;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Warehouse.Test.ServicesTests.Helper;

namespace Warehouse.Test.ServicesTests
{
    public class CustomerServiceTests
    {
        private ServiceHelper? _data;

        [Theory]
        [InlineData(1)]
        public void CustomerService_Get_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(x => x.CustomerRepository.Get(id))
                .Returns(_data.CustomersTest.First(s => s.Id == id));
            ICustomerService customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = customerService.Get(id);
            
            mockUnitOfWork.Verify(x => x.CustomerRepository.Get(id), Times.Once);
            Assert.Equal(id, actual.Id);
            Assert.Equal(_data.CustomersTest.First(s => s.Id == id).Name, actual.Name);
            Assert.Equal(_data.CustomersTest.First(s => s.Id == id).Surname, actual.Surname);
            Assert.Equal(_data.CustomersTest.First(s => s.Id == id).PhoneNumber, actual.PhoneNumber);
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData(100)]
        public void CustomerService_Get_ShouldReturnNotFoundException(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Get(id));
            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => customerService.Get(id));
        }

        [Fact]
        public void CustomerService_GetALl_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.GetAll())
                .Returns(_data.CustomersTest);

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);

            var actual = customerService.GetAll().ToList();

            mockUnitOfWork.Verify(x => x.CustomerRepository.GetAll(), Times.Once);
            Assert.Equal(actual.Count, _data.CustomersTest.Count);
            Assert.NotNull(actual);

        }
        [Fact]
        public void CustomerService_Create_ShouldReturnCorrectValue()
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Create(It.IsAny<Customer>()));
            var itemRequest = new CustomerRequest { Name = "New", Surname = "New", PhoneNumber = "+380090909" };

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);
            customerService.Create(itemRequest);


            mockUnitOfWork.Verify(x => x.CustomerRepository.Create(It.IsAny<Customer>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void CustomerService_Update_ShouldReturnCorrectValue()
        {
            int id = 1;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Update(It.IsAny<Customer>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.Get(id))
            .Returns(_data.CustomersTest.First(s => s.Id == id));
            var itemRequest = new CustomerRequest { Name = "New", Surname = "New", PhoneNumber = "+380090909" };

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);
            customerService.Update(itemRequest, id);

            mockUnitOfWork.Verify(x => x.CustomerRepository.Get(id), Times.Once);
            mockUnitOfWork.Verify(x => x.CustomerRepository.Update(It.IsAny<Customer>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);


        }

        [Fact]
        public void CustomerService_Update_ShouldReturnNotFoundExeption()
        {
            int id = 100;
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Update(It.IsAny<Customer>()));
            mockUnitOfWork.Setup(x => x.CustomerRepository.Get(id));
            var itemRequest = new CustomerRequest { Name = "New", Surname = "New", PhoneNumber = "+380090909" };

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => customerService.Update(itemRequest, id));
        }

        [Theory]
        [InlineData(2)]
        public void CustomerService_Delete_ShouldReturnCorrectValue(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Delete(id));
            mockUnitOfWork
            .Setup(x => x.CustomerRepository.Get(id))
                .Returns(_data.CustomersTest.First(s => s.Id == id));

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);
            customerService.Delete(id);

            mockUnitOfWork.Verify(x => x.CustomerRepository.Delete(id), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public void CustomerService_Delete_ShouldReturnNotFoundExeption(int id)
        {
            _data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.CustomerRepository.Delete(id));

            var customerService = new CustomerService(mockUnitOfWork.Object, _data.MapperProfile);

            Assert.Throws<NotFoundException>(() => customerService.Delete(id));
        }
    }
}

