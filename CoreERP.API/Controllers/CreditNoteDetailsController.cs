using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditNoteDetailController : Controller
    {
        private readonly ICreditNoteDetailRepository _CreditNoteDetailRepository;

        public CreditNoteDetailController(ICreditNoteDetailRepository littleBoxDeatilsRepository)
        {
            _CreditNoteDetailRepository = littleBoxDeatilsRepository;
        }

        //[HttpGet("{accountID}")]
        //public async Task<IActionResult> GetAllCreditNoteDetails(int accountID)
        //{
        //    return Ok(await _CreditNoteDetailRepository.GetAllCreditNoteDetails(accountID));
        //}

        [HttpGet("{CreditNoteID}")]
        public async Task<IActionResult> GetCreditNoteDetailDetails(int CreditNoteID)
        {
            return Ok(await _CreditNoteDetailRepository.GetAllCreditNoteDetails(CreditNoteID));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCreditNoteDetail([FromBody] CreditNoteDetails creditNoteDetail)
        {
            if (creditNoteDetail == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _CreditNoteDetailRepository.InsertCreditNoteDetails(creditNoteDetail);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCreditNoteDetail([FromBody] CreditNoteDetails creditNoteDetail)
        {
            if (creditNoteDetail == null)
                return BadRequest();

            if (creditNoteDetail.monto.ToString().Trim() == string.Empty)
            {
                ModelState.AddModelError("CreditNoteDetail", "Account number shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _CreditNoteDetailRepository.UpdateCreditNoteDetails(creditNoteDetail);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditNoteDetail(int id)
        {
            if (id == 0)
                return BadRequest();

            await _CreditNoteDetailRepository.DeleteCreditNoteDetails(id);

            return NoContent(); //success
        }
    }
}
