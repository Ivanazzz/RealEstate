using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Attributes;
using RealEstate.Models.Dtos.EstateDtos;
using RealEstate.Repositories.Contracts;
using System.Security.Claims;

namespace RealEstate.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [AllowAnonymous]
    public class EstateController : ControllerBase
    {
        private readonly IEstateRepository estateRepository;

        public EstateController(IEstateRepository estateRepository)
        {
            this.estateRepository = estateRepository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAsync()
        {
            var estates = await estateRepository.GetAllAsync();

            return Ok(estates);
        }

        [HttpGet("Filtered")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] EstateFilterDto estateFilter)
        {
            var estates = await estateRepository.GetFilteredAsync(estateFilter);

            return Ok(estates);
        }

        [HttpPost("Add")]
        [AuthorizedBroker]
        public async Task<IActionResult> AddAsync([FromBody] EstateAddDto estateDto)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await estateRepository.AddAsync(username, estateDto);

            return Ok();
        }

        [HttpGet("ById/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var estate = await estateRepository.GetByIdAsync(id);

            return Ok(estate);
        }

        [HttpPost("Update")]
        [AuthorizedBroker]
        public async Task<IActionResult> UpdateAsync([FromBody] EstateUpdateDto estateDto)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await estateRepository.UpdateAsync(username, estateDto);

            return Ok();
        }
    }
}
