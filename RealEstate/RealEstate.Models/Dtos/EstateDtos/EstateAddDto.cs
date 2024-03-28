using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.EstateDtos
{
    public class EstateAddDto
    {
        public EstateType Type { get; set; }

        public decimal Price { get; set; }

        public int Size { get; set; }

        public int SettlementId { get; set; }

        public int? Floor { get; set; }
    }
}
