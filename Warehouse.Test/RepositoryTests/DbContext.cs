using DAL;
using DAL.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Test.RepositoryTests
{
    public class DbContext
    {
        public static DbContextOptions<WarehouseContext> GetWarehouseDbOption()
        {
            var options = new DbContextOptionsBuilder<WarehouseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new WarehouseContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static void SeedData(WarehouseContext context)
        {
            AddTypeOfGoodData(context);
            AddCustomerData(context);
            AddGoodData(context, context.TypeOfGoods.ToList());
            AddOrderData(context);
            AddQueueData(context);

        }
        public static void AddTypeOfGoodData(WarehouseContext context)
        {
            var typeOfGoods = new List<TypeOfGood>
            {
                new TypeOfGood { Name = "Wood"},
                new TypeOfGood { Name = "Steel"},
                new TypeOfGood { Name = "For delete" }
            };
            context.TypeOfGoods.AddRange(typeOfGoods);
            context.SaveChanges();
        }
        public static void AddCustomerData(WarehouseContext context)
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Anna", Surname = "Hatuwey", PhoneNumber = "+380777666"},
                new Customer { Name = "Sophie", Surname = "Richi", PhoneNumber = "+380666777"},
                new Customer { Name = "For delete", Surname = "For delete", PhoneNumber = "+380000000"}
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
        public static void AddGoodData(WarehouseContext context, List<TypeOfGood> typeOfGood )
        {
            var goods = new List<Good>
            {
                new Good { Name = "Stick", Price = 50, Amount = 10, TypeOfGoodId = 1, TypeOfGood = typeOfGood[0]},
                new Good { Name = "Iron ore", Price = 100, Amount = 15, TypeOfGoodId = 2, TypeOfGood = typeOfGood[1]},
                new Good { Name = "For delete", Price = 1, Amount = 1, TypeOfGoodId = 1, TypeOfGood = typeOfGood[0]}
            };
            context.Goods.AddRange(goods);
            context.SaveChanges();
        }
        public static void AddOrderData(WarehouseContext context)
        {
            var orders = new List<Order>
            {
                new Order { GoodId = 1, CustomerId = 1, Amount = 5, TotalPrice = 250,  StatusOfOrder = StatusOfOrder.Done, TimeCreated = DateTime.Now},
                new Order { GoodId = 2, CustomerId = 2, Amount = 20, TotalPrice = 0,  StatusOfOrder = StatusOfOrder.Active, TimeCreated = DateTime.Now},
                new Order { GoodId = 3, CustomerId = 2, Amount = 40, TotalPrice = 0,  StatusOfOrder = StatusOfOrder.Active, TimeCreated = DateTime.Now}

            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
        public static void AddQueueData(WarehouseContext context)
        {
            var queue = new List<Queue>
            {
                new Queue { OrderId = 2},
                new Queue { OrderId = 3},
            };
            context.Queues.AddRange(queue);
            context.SaveChanges();
        }
    }
}
