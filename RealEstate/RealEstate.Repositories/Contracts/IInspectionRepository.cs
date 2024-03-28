using RealEstate.Models.Dtos.InspectionDtos;
using RealEstate.Models.Enums;

namespace RealEstate.Repositories.Contracts
{
    public interface IInspectionRepository
    {
        public Task AddAsync(string username, InspectionAddDto inspectionDto);

        public Task<List<InspectionGetDto>> GetAllAsync(string username);

        public Task ChangeStatusAsync(string username, int inspectionId, Status status);

        Task<List<InspectionGetDto>> GetFilteredAsync(InspectionFilterDto inspectionFilter);
    }
}
