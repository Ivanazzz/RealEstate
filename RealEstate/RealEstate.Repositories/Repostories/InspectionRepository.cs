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
                InspectionDate = DateTime.SpecifyKind(inspectionDto.InspectionDate, DateTimeKind.Utc),
                EstateId = inspectionDto.EstateId,
                Status = Status.Stated
            };

            await context.AddAsync(inspection);
            await context.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(string username, int inspectionId, Status status)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            if (user.Role != Role.Broker)
            {
                throw new BadRequestException("Не сте физическо лице");
            }

            if (inspectionId == 0)
            {
                throw new NotFoundException("Невалиден оглед");
            }

            var inspection = await context.Inspections
                .Include(i => i.Estate)
                .SingleOrDefaultAsync(i => i.Id == inspectionId);

            if (inspection.Estate.EstateBrokerId != user.Id)
            {
                throw new BadRequestException("Не сте брокер на този имот");
            }

            inspection.Status = status;

            await context.SaveChangesAsync();
        }

        public async Task<List<InspectionGetDto>> GetAllAsync(string username)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            var inspections = context.Inspections
                .Include(i => i.Estate)
                .Include(i => i.User);

            if (user.Role == Role.Individual)
            {
                inspections.Where(i => i.UserId == user.Id);
            }

            if (user.Role == Role.Broker)
            {
                //var brokerEstateIds = await context.Estates
                //    .Where(e => e.EstateBrokerId == user.Id)
                //    .Select(e => e.Id)
                //    .ToListAsync();

                //inspections.Where(i => brokerEstateIds.Contains(i.EstateId));

                inspections.Where(i => i.Estate.EstateBrokerId == user.Id);
            }

            var inspectionDtos = await inspections.Select(i => new InspectionGetDto
            {
                Id = i.Id,
                UserId = i.UserId,
                Username = i.User.Username,
                InspectionDate = i.InspectionDate,
                EstateId = i.EstateId,
                Status = i.Status
            })
            .ToListAsync();

            return inspectionDtos;
        }
    }
}
