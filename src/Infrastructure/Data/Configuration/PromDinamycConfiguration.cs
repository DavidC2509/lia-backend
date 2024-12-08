using Lia.Core.PromDinamicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lia.Infrastructure.Data.Configuration
{
    public class PromDinamycConfiguration : IEntityTypeConfiguration<PromDinamyc>
    {
        public void Configure(EntityTypeBuilder<PromDinamyc> builder)
        {
            builder.ToTable("PromDinamyc");

            // Configure the Id property to use the database default value for new entities
            builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");

            builder.HasKey(e => e.Id);

        }
    }
}