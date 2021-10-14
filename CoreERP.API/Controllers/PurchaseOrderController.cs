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
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderRepository _PurchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderTypeRepository)
        {
            _PurchaseOrderRepository = purchaseOrderTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseOrders()
        {
            return Ok(await _PurchaseOrderRepository.GetAllPurchaseOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseOrderDetails(int id)
        {
            return Ok(await _PurchaseOrderRepository.GetPurchaseOrderDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PurchaseOrderRepository.InsertPurchaseOrder(purchaseOrder);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePurchaseOrder([FromBody] PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                return BadRequest();

            if (purchaseOrder.id_orden_compra == 0)
            {
                ModelState.AddModelError("PurchaseOrder", "PurchaseOrder name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PurchaseOrderRepository.UpdatePurchaseOrder(purchaseOrder);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrder(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PurchaseOrderRepository.DeletePurchaseOrder(id);

            return NoContent(); //success
        }
    }
}
