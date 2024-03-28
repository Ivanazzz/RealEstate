using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models.Enums;

namespace RealEstate.Models.Entities
{
    public class Estate
    {
        public int Id { get; set; }

        public int EstateBrokerId { get; set; }

        public User EstateBroker { get; set; }

        public EstateType Type { get; set; }

        public decimal Price { get; set; }

        public int Size { get; set; }

        public int SettlementId { get; set; }

        public Settlement Settlement { get; set; }

        public int? Floor { get; set; }
    }

    public class EstateConfiguration : IEntityTypeConfiguration<Estate>
    {
        public void Configure(EntityTypeBuilder<Estate> builder)
        {
            builder
                .Property(b => b.Price)
                .HasPrecision(18,2);

            builder
                .Property(b => b.Floor)
                .HasDefaultValue(null);
        }
    }
}
