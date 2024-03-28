using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models.Entities
{
    public class Settlement
    {
        public int Id { get; set; }

        public string Ekatte { get; set; }

        public string Name { get; set; }
    }

    public class SettlementConfiguration : IEntityTypeConfiguration<Settlement>
    {
        public void Configure(EntityTypeBuilder<Settlement> builder)
        {
            builder
                .Property(b => b.Ekatte)
                .IsRequired();

            builder
                .Property(b => b.Name)
                .IsRequired();
        }
    }
}
