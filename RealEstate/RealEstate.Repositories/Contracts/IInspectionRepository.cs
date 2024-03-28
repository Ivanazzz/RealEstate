using RealEstate.Models.Dtos.InspectionDtos;

namespace RealEstate.Repositories.Contracts
{
    public interface IInspectionRepository
    {
        public Task AddAsync(string username, InspectionAddDto inspectionDto);
    }
}
