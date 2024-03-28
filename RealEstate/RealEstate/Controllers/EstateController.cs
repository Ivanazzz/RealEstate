using Microsoft.AspNetCore.Mvc;
using RealEstate.Repositories.Contracts;

namespace RealEstate.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateRepository estateRepository;

        public EstateController(IEstateRepository estateRepository)
        {
            this.estateRepository = estateRepository;
        }
    }
}
