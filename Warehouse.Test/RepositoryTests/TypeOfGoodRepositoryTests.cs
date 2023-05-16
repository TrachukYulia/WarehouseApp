using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Repository;

namespace Warehouse.Test.RepositoryTests
{
    public class TypeOfGoodRepositoryTests
    {
        private readonly List<TypeOfGood> expectedTypeOfGood = new List<TypeOfGood>
        {
            new TypeOfGood { Id = 1, Name = "Wood" },
            new TypeOfGood { Id = 2, Name = "Steel" },
            new TypeOfGood { Id = 3, Name = "For delete"}
        };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TypeOfGoodRepository_Get_ShouldReturnCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var typeOfGoodRepository = new TypeOfGoodRepository(context);
            var expected = expectedTypeOfGood.FirstOrDefault(x => x.Id == id);

            var actual = typeOfGoodRepository.Get(id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void GetTypeOfGoodRepository_Getter_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            UnitOfWork test = new UnitOfWork(context);

            _ = Assert.IsAssignableFrom<TypeOfGoodRepository>(test.TypeOfGoodRepository);
        }

        [Fact]
        public void TypeOfGoodRepository_GetAll_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var typeOfGoodRepository = new TypeOfGoodRepository(context);
            var expected = expectedTypeOfGood;

            var actual = typeOfGoodRepository.GetAll().ToList();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void TypeOfGoodRepository_Update_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var typeOfGoodRepository = new TypeOfGoodRepository(context);
            var typeOfGood = new TypeOfGood { Id = 1, Name = "New type" };
            var expected = typeOfGood;

            typeOfGoodRepository.Update(typeOfGood);
            var actual = context.TypeOfGoods.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(actual.Name, expected.Name);
        }

        [Theory]
        [InlineData(3)]
        public void TypeOfGoodRepository_Delete_ShouldReturnInCorrectValue(int id)
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var typeOfGoodRepository = new TypeOfGoodRepository(context);

            typeOfGoodRepository.Delete(id);
            context.SaveChanges();
            var actual = context.TypeOfGoods.Contains(context.TypeOfGoods.FirstOrDefault(x => x.Id == id));

            Assert.False(actual);
        }

        [Fact]
        public void TypeOfGoodRepository_Create_ShouldReturnCorrectValue()
        {
            using var context = new WarehouseContext(DbContext.GetWarehouseDbOption());
            var typeOfGoodRepository = new TypeOfGoodRepository(context);
            var itemToAdd = new TypeOfGood
            {
                Name = "For add"
            };

            typeOfGoodRepository.Create(itemToAdd);
            context.SaveChanges();
            var actual = context.TypeOfGoods.Contains(itemToAdd);

            Assert.True(actual);
        }
    }
}
