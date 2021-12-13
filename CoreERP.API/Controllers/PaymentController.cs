using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _PaymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _PaymentRepository = paymentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await _PaymentRepository.GetAllPayments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetails(int id)
        {
            return Ok(await _PaymentRepository.GetPaymentDetails(id));
        }

        [HttpGet]
        [Route("GetPaymentDetailsByPurchaseID/{id}")]
        public async Task<IActionResult> GetPaymentDetailsByPurchaseID(int id)
        {
            return Ok(await _PaymentRepository.GetPaymentDetailsByPurchaseID(id));
        }

        [HttpGet("{id}")]
        [Route("GetPaymentDetailsByGeneralPurchaseID/{id}")]
        public async Task<IActionResult> GetPaymentDetailsByGeneralPurchaseID(int id)
        {
            return Ok(await _PaymentRepository.GetPaymentDetailsByGeneralPurchaseID(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (payment == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PaymentRepository.InsertPayment(payment);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment payment)
        {
            if (payment == null)
                return BadRequest();

            if (payment.estado.Trim() == string.Empty)
            {
                ModelState.AddModelError("Payment", "Payment status shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PaymentRepository.UpdatePayment(payment);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PaymentRepository.DeletePayment(id);

            return NoContent(); //success
        }

    }
}
