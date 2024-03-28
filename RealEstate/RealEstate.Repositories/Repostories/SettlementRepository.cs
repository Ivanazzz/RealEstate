using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.Dtos.SettlementDtos;
using RealEstate.Repositories.Contracts;

namespace RealEstate.Repositories.Repostories
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly RealEstateDbContext context;

        public SettlementRepository(RealEstateDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SettlementGetDto>> GetAllAsync()
        {
            var settlements = await context.Settlements
                .Select(s => new SettlementGetDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();

            return settlements;
        }
    }
}
