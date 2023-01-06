using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;

        public ProductController(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _ProductRepository.GetAllProducts());
        }

        [HttpGet("definitions")]
        public async Task<IActionResult> GetProductsDefinitions()
        {
            return Ok(await _ProductRepository.GetProductsDefinitions());
        }

        [HttpPost("[action]")]
        [Route("image")]
        public async Task<string> Save()
        {
            string path = string.Empty;
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    path = Path.Combine("C:\\Users\\cbalbuena\\source\\repos\\Lugaro\\IdentityTEST\\bin\\Release\\netcoreapp3.1\\publish\\wwwroot\\images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            byte[] ByteArray = System.IO.File.ReadAllBytes(path);

            return Convert.ToBase64String(ByteArray);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            return Ok(await _ProductRepository.GetProductDetails(id));
        }

        [HttpGet]
        [Route("GetProductDetailsByCode/{code}")]
        public async Task<IActionResult> GetProductDetailsByCode(string code)
        {
            return Ok(await _ProductRepository.GetProductDetailsByCode(code));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _ProductRepository.InsertProduct(product);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            if (product.producto.Trim() == string.Empty)
            {
                ModelState.AddModelError("Product", "Product name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ProductRepository.UpdateProduct(product);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            await _ProductRepository.DeleteProduct(id);

            return NoContent(); //success
        }
    }
}
