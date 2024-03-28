using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos
{
    public class EstateGetDto
    {
        public int Id { get; set; }

        public string EstateBrokerUsername { get; set; }

        public EstateType Type { get; set; }

        public decimal Price { get; set; }

        public int Size { get; set; }

        public string SettlementName { get; set; }

        public int? Floor { get; set; }
    }
}
