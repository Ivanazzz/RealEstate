using RealEstate.Models.Enums;

namespace RealEstate.Models.Entities
{
    public class Inspection
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime InspectionDate { get; set; }

        public int EstateId { get; set; }

        public Estate Estate { get; set; }

        public Status Status { get; set; } 
    }
}
