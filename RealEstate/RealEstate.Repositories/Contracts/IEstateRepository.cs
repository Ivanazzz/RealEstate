using RealEstate.Models.Dtos;

namespace RealEstate.Repositories.Contracts
{
    public interface IEstateRepository
    {
        public Task<List<EstateGetDto>> GetAll();
    }
}
