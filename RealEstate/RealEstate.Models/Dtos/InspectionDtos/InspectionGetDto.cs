using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.InspectionDtos
{
    public class InspectionGetDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public DateTime InspectionDate { get; set; }

        public int EstateId { get; set; }

        public Status Status { get; set; }
    }
}
