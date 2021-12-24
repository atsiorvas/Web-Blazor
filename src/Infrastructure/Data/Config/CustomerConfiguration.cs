using BlazorApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.Infrastructure.Data.Config {

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer> {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.Property(b => b.Id)
                .IsRequired()
               .HasDefaultValueSql("newsequentialid()");
            builder.Property(b => b.CompanyName)
              .HasColumnType("nvarchar(max)");
            builder.Property(b => b.ContactName)
              .HasColumnType("nvarchar(max)");
            builder.Property(b => b.Address)
              .HasColumnType("nvarchar(max)");
            builder.Property(b => b.City)
              .HasColumnType("nvarchar(255)");
            builder.Property(b => b.Region)
             .HasColumnType("nvarchar(255)");
            builder.Property(b => b.PostalCode)
            .HasColumnType("nvarchar(255)");
            builder.Property(b => b.Country)
                .HasColumnType("nvarchar(255)");
            builder.Property(b => b.Phone)
                .HasColumnType("nvarchar(255)");
        }
    }
}