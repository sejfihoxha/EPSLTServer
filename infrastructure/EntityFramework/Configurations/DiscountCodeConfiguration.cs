using EPSLTServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EPSLTTaskServer.Infrastructure.EntityFramework.Configurations
{
    public class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code)
                   .IsUnique();

            builder.ToTable("DiscountCodes");
        }
    }
}
