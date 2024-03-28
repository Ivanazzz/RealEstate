using RealEstate.Models.Entities;
using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.EstateDtos
{
    public class EstateFilterDto
    {

        public EstateType? Type { get; set; }

        public int PriceMin { get; set; }

        public int PriceMax { get; set; }

        public int SizeMin { get; set; }

        public int SizeMax { get; set; }

        public int SettlementId { get; set; }

        public int Floor { get; set; }

        public IQueryable<Estate> WhereBuilder(IQueryable<Estate> query)
        {
            if (Type.HasValue)
            {
                query = query.Where(s => s.Type == Type);
            }

            if (PriceMin != 0)
            {
                query = query.Where(p => p.Price >= PriceMin);
            }

            if (PriceMax != 0)
            {
                query = query.Where(p => p.Price <= PriceMax);
            }

            if (SizeMin != 0)
            {
                query = query.Where(p => p.Size >= SizeMin);
            }

            if (SizeMax != 0)
            {
                query = query.Where(p => p.Size <= SizeMax);
            }

            if (SettlementId > 0)
            {
                query = query.Where(s => s.SettlementId == SettlementId);
            }

            if (Floor != 0)
            {
                query = query.Where(s => s.Floor == Floor);
            }

            return query;
        }
    }
}
