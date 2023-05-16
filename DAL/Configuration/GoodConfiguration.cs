using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DAL.Configuration
{
    internal class GoodConfiguration: IEntityTypeConfiguration<Good>
    {
        public void Configure(EntityTypeBuilder<Good> builder)
        {
            builder.HasOne(x => x.TypeOfGood)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.TypeOfGoodId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Price).IsRequired();
        }

    }
}
