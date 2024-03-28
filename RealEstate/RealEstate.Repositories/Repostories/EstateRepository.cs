using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.Dtos;
using RealEstate.Repositories.Contracts;

namespace RealEstate.Repositories.Repostories
{
    public class EstateRepository : IEstateRepository
    {
        private readonly RealEstateDbContext context;

        public EstateRepository(RealEstateDbContext context)
        {
            this.context = context;
        }

        public async Task<List<EstateGetDto>> GetAll()
        {
            var estates = await context.Estates
                .Include(e => e.EstateBroker)
                .Include(e => e.Settlement)
                .Select(e => new EstateGetDto
                {
                    Id = e.Id,
                    EstateBrokerUsername = e.EstateBroker.Username,
                    Type = e.Type,
                    Price = e.Price,
                    Size = e.Size,
                    SettlementName = e.Settlement.Name,
                    Floor = e.Floor
                })
                .ToListAsync();

            return estates;
        }
    }
}
