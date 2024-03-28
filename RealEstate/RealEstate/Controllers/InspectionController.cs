using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Attributes;
using RealEstate.Models.Dtos.InspectionDtos;
using RealEstate.Models.Enums;
using RealEstate.Repositories.Contracts;
using RealEstate.Repositories.Repostories;
using System.Security.Claims;

namespace RealEstate.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionRepository inspectionRepository;

        public InspectionController(IInspectionRepository inspectionRepository)
        {
            this.inspectionRepository = inspectionRepository;
        }

        [HttpPost("Add")]
        [AuthorizedIndividual]
        public async Task<IActionResult> AddAsync([FromBody] InspectionAddDto inspectionDto)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await inspectionRepository.AddAsync(username, inspectionDto);

            return Ok();
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAsync()
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var estates = await inspectionRepository.GetAllAsync(username);

            return Ok(estates);
        }

        [HttpPost("ChangeStatus")]
        [AuthorizedBroker]
        public async Task<IActionResult> ChangeStatusAsync([FromQuery] int inspectionId, Status status)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await inspectionRepository.ChangeStatusAsync(username, inspectionId, status);

            return Ok();
        }
    }
}
