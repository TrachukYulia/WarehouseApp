using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration
{
    public class TypeOfGoodConfiguration : IEntityTypeConfiguration<TypeOfGood>
    {
        public void Configure(EntityTypeBuilder<TypeOfGood> builder)
        {
           builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
        }
    }
}
