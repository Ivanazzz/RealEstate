using RealEstate.Models.Entities;
using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.InspectionDtos
{
    public class InspectionFilterDto
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Status? InspectionStatus { get; set; }

        public IQueryable<Inspection> WhereBuilder(IQueryable<Inspection> query)
        {
            if (InspectionStatus.HasValue)
            {
                query = query.Where(s => s.Status == InspectionStatus);
            }

            if (DateFrom != null)
            {
                var dateFromUtc = DateFrom.Value.ToUniversalTime();
                query = query.Where(p => p.InspectionDate >= dateFromUtc);
            }

            if (DateTo != null)
            {
                var dateToUtc = DateTo.Value.ToUniversalTime();
                query = query.Where(p => p.InspectionDate <= dateToUtc);
            }

            return query;
        }
    }
}
