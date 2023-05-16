using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DAL.Configuration
{
    internal class QueueConfiguration : IEntityTypeConfiguration<Queue>
    {
        public void Configure(EntityTypeBuilder<Queue> builder)
        {

            builder.HasOne(x => x.Orders)
                .WithMany(x => x.Queue)
                .HasForeignKey(x => x.OrderId);
              //  .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
