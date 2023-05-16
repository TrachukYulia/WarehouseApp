using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DAL.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders )
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Goods)
             .WithMany(x => x.Orders)
             .HasForeignKey(x => x.GoodId)
             .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
