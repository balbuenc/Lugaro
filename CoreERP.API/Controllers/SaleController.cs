using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class SaleController : Controller
    {

        private readonly ISaleRepository _SaleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _SaleRepository = saleRepository;
        }


        [HttpGet]
        [Route("GetInvoiceNumbers")]
        public async Task<IActionResult> GetInvoiceNumbers()
        {
            return Ok(await _SaleRepository.GetInvoiceNumbers());
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            return Ok(await _SaleRepository.GetAllSales());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleDetails(int id)
        {
            return Ok(await _SaleRepository.GetSaleDetails(id));
        }

        [HttpGet]
        [Route("GetSaleByInvoice/{invoice_number}")]
        public async Task<IActionResult> GetSaleDetailsByInvoice(string invoice_number)
        {
            return Ok(await _SaleRepository.GetSaleDetailsByInvoice(invoice_number));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Budget budget)
        {
            if (budget == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _SaleRepository.GenerateSales(budget));



        }

        [HttpPut]
        public async Task<IActionResult> UpdateSale([FromBody] Sale sale)
        {
            if (sale == null)
                return BadRequest();

            if (sale.factura.ToString() == string.Empty)
            {
                ModelState.AddModelError("Sale", "Sale Invoice shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _SaleRepository.UpdateSale(sale);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (id == 0)
                return BadRequest();

            await _SaleRepository.DeleteSale(id);

            return NoContent(); //success
        }

    }
}
