using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInvoiceController : Controller
    {
        private readonly IClientInvoiceRepository _ClientInvoiceRepository;

        public ClientInvoiceController(IClientInvoiceRepository areaRepository)
        {
            _ClientInvoiceRepository = areaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientInvoices()
        {
            return Ok(await _ClientInvoiceRepository.GetAllClientInvoices());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientInvoiceDetails(int id)
        {
            return Ok(await _ClientInvoiceRepository.GetClientInvoiceDetails(id));
        }

        

      
    }
}
