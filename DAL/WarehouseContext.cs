using DAL.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using DAL.Configuration;



namespace DAL
{
    public class WarehouseContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<TypeOfGood> TypeOfGoods { get; set; }
        public virtual DbSet<Queue> Queues { get; set; }

        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new QueueConfiguration().Configure(modelBuilder.Entity<Queue>());
            new TypeOfGoodConfiguration().Configure(modelBuilder.Entity<TypeOfGood>());
            new GoodConfiguration().Configure(modelBuilder.Entity<Good>());
            DataSeed(modelBuilder);
        }
        public void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                   new Customer { Id = 1, Name = "Tom", Surname = "Soyer", PhoneNumber = "+3809192937" },
                   new Customer { Id = 2, Name = "Bob", Surname = "Karl", PhoneNumber = "+3802397489" },
                   new Customer { Id = 3, Name = "Sam", Surname = "Taylor", PhoneNumber = "+3808398269" }
           );
          modelBuilder.Entity<TypeOfGood>().HasData(
          new TypeOfGood { Id = 1, Name = "Wood" },
          new Good { Id = 2, Name = "Concrete" },
          new Good { Id = 3, Name = "Plastic" }
         );
            modelBuilder.Entity<Good>().HasData(
                  new Good { Id = 1, Name = "Brick", Amount = 10, Price = 30, TypeOfGoodId = 2 },
                  new Good { Id = 2, Name = "Concrete slab", Amount = 5, Price = 100, TypeOfGoodId = 2 },
                  new Good { Id = 3, Name = "Woden bars", Amount = 7, Price = 50, TypeOfGoodId = 1 },
                  new Good { Id = 4, Name = "Drywall", Amount = 20, Price = 30, TypeOfGoodId = 3 },
                  new Good { Id = 5, Name = "Plastic pipes", Amount = 30, Price = 45, TypeOfGoodId = 3 }
           );
        }
    }
}