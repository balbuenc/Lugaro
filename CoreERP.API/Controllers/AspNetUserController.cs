using CoreERP.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserController : Controller
    {
        private readonly IAspNetUserRepository _AspNetUserRepository;

        public AspNetUserController(IAspNetUserRepository AspNetUserTypeRepository)
        {
            _AspNetUserRepository = AspNetUserTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAspNetUsers()
        {
            return Ok(await _AspNetUserRepository.GetAllAspNetUsers());
        }
    }
}
