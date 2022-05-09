using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CreditNoteController : Controller
    {
        private readonly ICreditNoteRepository _CreditNoteRepository;

        public CreditNoteController(ICreditNoteRepository accountTypeRepository)
        {
            _CreditNoteRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCreditNotes()
        {
            return Ok(await _CreditNoteRepository.GetAllCreditNotes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditNoteDetails(int id)
        {
            return Ok(await _CreditNoteRepository.GetCreditNoteDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCreditNote([FromBody] CreditNote creditNote)
        {
            if (creditNote == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CreditNoteRepository.InsertCreditNote(creditNote);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCreditNote([FromBody] CreditNote creditNote)
        {
            if (creditNote == null)
                return BadRequest();

            if (creditNote.nro_nota.Trim() == string.Empty)
            {
                ModelState.AddModelError("CreditNote", "CreditNote number shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CreditNoteRepository.UpdateCreditNote(creditNote);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditNote(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CreditNoteRepository.DeleteCreditNote(id);

            return NoContent(); //success
        }
    }
}

