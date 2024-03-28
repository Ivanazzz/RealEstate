using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.EstateDtos
{
    public class EstateDetailsDto
    {
        public int Id { get; set; }

        public int EstateBrokerId { get; set; }

        public string EstateBrokerUsername { get; set; }

        public EstateType Type { get; set; }

        public decimal Price { get; set; }

        public int Size { get; set; }

        public int SettlementId { get; set; }

        public string SettlementName { get; set; }

        public int? Floor { get; set; }
    }
}
