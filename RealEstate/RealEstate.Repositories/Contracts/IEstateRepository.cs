using RealEstate.Models.Dtos.EstateDtos;

namespace RealEstate.Repositories.Contracts
{
    public interface IEstateRepository
    {
        public Task<List<EstateGetDto>> GetAllAsync();

        Task<List<EstateGetDto>> GetFilteredAsync(EstateFilterDto estateFilter);

        public Task AddAsync(string username, EstateAddDto estateDto);

        public Task UpdateAsync(string username, EstateUpdateDto estateDto);

        public Task<EstateDetailsDto> GetByIdAsync(int id);
    }
}
