using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.CustomExceptions;
using RealEstate.Models.Dtos.InspectionDtos;
using RealEstate.Models.Entities;
using RealEstate.Models.Enums;
using RealEstate.Repositories.Contracts;

namespace RealEstate.Repositories.Repostories
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly RealEstateDbContext context;

        public InspectionRepository(RealEstateDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(string username, InspectionAddDto inspectionDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            if (user.Role != Role.Individual)
            {
                throw new BadRequestException("Не сте физическо лице");
            }

            if (inspectionDto == null)
            {
                throw new BadRequestException("Невалиден оглед");
            }

            var inspection = new Inspection
            {
                UserId = user.Id,
                InspectionDate = inspectionDto.InspectionDate,
                EstateId = inspectionDto.EstateId,
                Status = Status.Stated
            };

            await context.AddAsync(inspection);
            await context.SaveChangesAsync();
        }
    }
}
