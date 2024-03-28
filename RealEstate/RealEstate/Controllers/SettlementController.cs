using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Repositories.Contracts;

namespace RealEstate.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [AllowAnonymous]
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementRepository settlementRepository;

        public SettlementController(ISettlementRepository settlementRepository)
        {
            this.settlementRepository = settlementRepository;
        }

        public async Task<IActionResult> GetAsync()
        {
            var settlements = await settlementRepository.GetAllAsync();

            return Ok(settlements);
        }
    }
}
