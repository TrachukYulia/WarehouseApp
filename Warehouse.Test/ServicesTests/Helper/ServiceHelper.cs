using AutoMapper;
using BLL;
using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace Warehouse.Test.ServicesTests.Helper
{
    public class ServiceHelper
    {
        private readonly IMapper _mapper;
        private MapperConfiguration _configuration;
        public ServiceHelper()
        {
            var myProfile = new AutoMapperProfile();
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(_configuration) ;
        }
        public IMapper MapperProfile => new Mapper(_configuration);
        public IMapper CreateMapperProfile()
        {
            var mapperProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));

            return new Mapper(configuration);
        }

        public List<TypeOfGood> TypeOfGoodsTest = new List<TypeOfGood>()
        {
                new TypeOfGood {Id = 1,  Name = "Wood"},
                new TypeOfGood {Id = 2,  Name = "Wood 2.0"},
                new TypeOfGood {Id = 3,  Name = "Plastic" }
        };

        public List<Good> GoodsTest = new List<Good>()
        {
                new Good {Id = 1, Name = "Stick", Price = 50, Amount = 10, TypeOfGoodId = 1},
                new Good {Id = 2,  Name = "Iron ore", Price = 100, Amount = 15, TypeOfGoodId = 2},
                new Good {Id = 3,  Name = "Plasit stick", Price = 20, Amount = 10, TypeOfGoodId = 3}
        };

        public List<Customer> CustomersTest = new List<Customer>()
        {
                new Customer { Id = 1, Name = "Anna", Surname = "Hatuwey", PhoneNumber = "+380777666" },
                new Customer { Id = 2, Name = "Sophie", Surname = "Richi", PhoneNumber = "+380666777" } ,
                new Customer { Id = 3, Name = "Max", Surname = "Roy", PhoneNumber = "+380000000" }
        };

        public List<Order> OrdersTest = new List<Order>()
        {
            new Order {Id = 1, GoodId = 1, CustomerId = 1, Amount = 5, TotalPrice = 250,  StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now},
            new Order {Id = 2, GoodId = 2, CustomerId = 2, Amount = 20, TotalPrice = 0,  StatusOfOrder = StatusOfOrder.Active, TimeCreated = DateTime.Now},
            new Order {Id = 3, GoodId = 3, CustomerId = 2, Amount = 5, TotalPrice = 100,  StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now},
            new Order {Id = 4,  GoodId = 4, CustomerId = 3, Amount = 10, TotalPrice = 200,  StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now}
        };
        public List<Queue> QueuesTest = new List<Queue>()
        {
                new Queue {Id = 1, OrderId = 2}
        };

        public List<TypeOfGoodModel> GetTypeOfGoodModelsTest => _mapper.Map<List<TypeOfGoodModel>>(TypeOfGoodsTest);
        public List<GoodModel> GetGoodModelsTest => _mapper.Map<List<GoodModel>>(GoodsTest);
        public List<OrderModel> GetOrderModelsTest => _mapper.Map<List<OrderModel>>(OrdersTest);
        public List<CustomerModel> GetCustomerModelsTest => _mapper.Map<List<CustomerModel>>(CustomersTest);
        public List<QueueModel> GetQueueModelsTest => _mapper.Map<List<QueueModel>>(QueuesTest);

    }
}
