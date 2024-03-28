using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.CustomExceptions;
using RealEstate.Models.Dtos.EstateDtos;
using RealEstate.Models.Entities;
using RealEstate.Models.Enums;
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

        public async Task AddAsync(string username, EstateAddDto estateDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            if(user.Role != Role.Broker)
            {
                throw new BadRequestException("Не сте брокер");
            }

            if (estateDto == null)
            {
                throw new BadRequestException("Невалиден имот");
            }

            if (estateDto.Type == EstateType.House || estateDto.Type == EstateType
                .Shop)
            {
                if (estateDto.Floor != 0 && estateDto.Floor != null)
                {
                    throw new BadRequestException("Имот от вид офис или къща не може да има етаж");
                }
            }

            var estate = new Estate
            {
                EstateBrokerId = user.Id,
                Type = estateDto.Type,
                Price = estateDto.Price,
                Size = estateDto.Size,
                SettlementId = estateDto.SettlementId,
                Floor = estateDto.Floor
            };

            await context.AddAsync(estate);
            await context.SaveChangesAsync();
        }

        public async Task<List<EstateGetDto>> GetAllAsync()
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

        public async Task<EstateDetailsDto> GetByIdAsync(int id)
        {
            var estate = await context.Estates
                .Include(e => e.EstateBroker)
                .Include(e => e.Settlement)
                .SingleOrDefaultAsync(e => e.Id == id);

            var estateDto = new EstateDetailsDto
            {
                Id = estate.Id,
                EstateBrokerId = estate.EstateBrokerId,
                EstateBrokerUsername = estate.EstateBroker.Username,
                Type = estate.Type,
                Price = estate.Price,
                Size = estate.Size,
                SettlementId = estate.SettlementId,
                SettlementName = estate.Settlement.Name,
                Floor = estate.Floor
            };

            return estateDto;
        }

        public async Task<List<EstateGetDto>> GetFilteredAsync(EstateFilterDto estateFilter)
        {
            var estates = await estateFilter
                .WhereBuilder(context.Estates)
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

        public async Task UpdateAsync(string username, EstateUpdateDto estateDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            if (user.Role != Role.Broker)
            {
                throw new BadRequestException("Не сте брокер");
            }

            if (user.Id != estateDto.EstateBrokerId)
            {
                throw new BadRequestException("Не сте брокер на този имот");
            }

            if (estateDto == null)
            {
                throw new BadRequestException("Невалиден имот");
            }

            if (estateDto.Type == EstateType.House || estateDto.Type == EstateType
                .Shop)
            {
                if (estateDto.Floor != 0 && estateDto.Floor != null)
                {
                    throw new BadRequestException("Имот от вид офис или къща не може да има етаж");
                }
            }

            var estate = context.Estates
                .SingleOrDefault(e => e.Id == estateDto.Id);

            if (estate == null)
            {
                throw new BadRequestException("Невалиден имот");
            }

            estate.Type = estateDto.Type;
            estate.Price = estateDto.Price;
            estate.Size = estateDto.Size;
            estate.SettlementId = estateDto.SettlementId;
            estate.Floor = estateDto.Floor;

            await context.SaveChangesAsync();
        }
    }
}
