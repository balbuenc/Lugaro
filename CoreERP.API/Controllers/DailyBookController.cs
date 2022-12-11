using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyBookController : Controller
    {
        private readonly IDailyBookRepository _DailyBookRepository;

        public DailyBookController(IDailyBookRepository DailyBookTypeRepository)
        {
            _DailyBookRepository = DailyBookTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyBooks()
        {
            return Ok(await _DailyBookRepository.GetAllDailyBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyBookDetails(int id)
        {
            return Ok(await _DailyBookRepository.GetDailyBookDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyBook([FromBody] DailyBook DailyBook)
        {
            if (DailyBook == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _DailyBookRepository.InsertDailyBook(DailyBook);

            return Created("created", created);

        }

        [HttpPost("{factura}")]
        [Route("GenerateDailyBook/{factura}")]
        public async Task<IActionResult> GenerateDailyBook(string factura)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _DailyBookRepository.GenerateDailyBook(factura);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDailyBook([FromBody] DailyBook DailyBook)
        {
            if (DailyBook == null)
                return BadRequest();

            if (DailyBook.cuenta.Trim() == string.Empty)
            {
                ModelState.AddModelError("DailyBook", "DailyBook name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _DailyBookRepository.UpdateDailyBook(DailyBook);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyBook(int id)
        {
            if (id == 0)
                return BadRequest();

            await _DailyBookRepository.DeleteDailyBook(id);

            return NoContent(); //success
        }
    }
}
