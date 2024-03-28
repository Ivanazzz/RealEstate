using RealEstate.Models.Dtos.SettlementDtos;

namespace RealEstate.Repositories.Contracts
{
    public interface ISettlementRepository
    {
        public Task<List<SettlementGetDto>> GetAllAsync();
    }
}
