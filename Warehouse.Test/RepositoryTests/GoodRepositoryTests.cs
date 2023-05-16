using DAL.Models;
using DAL.Repository;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Activation;

namespace Warehouse.Test.RepositoryTests
{
    public class GoodRepositoryTests
    {
        private readonly List<Good> expectedGood = new List<Good>
        {
           new Good { Id = 1,  Name = "Stick", Price = 50, Amount = 10, TypeOfGoodId = 1},
           new Good { Id = 2, Name = "Iron ore", Price = 100, Amount = 15, TypeOfGoodId = 2},
           new Good { Id = 3, Name = "For delete", Price = 1, Amount = 1, TypeOfGoodId = 1}
         };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GoodRepository_Get_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var goodRepository = new GoodRepository(context);
            var expected = expectedGood.FirstOrDefault(x => x.Id == id);

            var actual = goodRepository.Get(id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.TypeOfGoodId, actual.TypeOfGoodId);

        }
        [Fact]
        public void GetGoodRepository_Getter_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            UnitOfWork test = new UnitOfWork(context);

            _ = Assert.IsAssignableFrom<GoodRepository>(test.GoodRepository);
        }

        [Fact]
        public void GoodRepository_GetAll_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var goodRepository = new GoodRepository(context);
            var expected = expectedGood;

            var actual = goodRepository.GetAll().ToList();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GoodRepository_Update_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var goodRepository = new GoodRepository(context);
            var good = new Good {Id = 1, Name = "New", Price = 5, Amount = 10, TypeOfGoodId = 1 };
            var expected = good;

            goodRepository.Update(good);
            var actual = context.Goods.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Amount, actual.Amount);
            Assert.Equal(expected.TypeOfGoodId, actual.TypeOfGoodId);
        }

        [Theory]
        [InlineData(3)]
        public void GoodRepository_Delete_ShouldReturnInCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var goodRepository = new GoodRepository(context);

            goodRepository.Delete(id);
            context.SaveChanges();
            var actual = context.Goods.Contains(context.Goods.FirstOrDefault(x => x.Id == id));

            Assert.False(actual);
        }

        [Fact]
        public void GoodRepository_Create_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var goodRepository = new GoodRepository(context);
            var itemToAdd = new Good
            {
                Name = "For add", 
                Amount = 10, 
                Price  = 11,
                TypeOfGoodId = 1
            };

            goodRepository.Create(itemToAdd);
            context.SaveChanges();
            var actual = context.Goods.Contains(itemToAdd);

            Assert.True(actual);
        }

    }
}
