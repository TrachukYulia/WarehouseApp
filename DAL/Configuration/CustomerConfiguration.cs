using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DAL.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(o=>o.Name).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Surname).HasMaxLength(100);
            builder.Property(o => o.PhoneNumber).IsRequired();
        }
    }
}
