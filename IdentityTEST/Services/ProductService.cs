using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }

        public async Task<HttpResponseMessage> SaveImage(MultipartFormDataContent content)
        {

            return await _httpClient.PostAsync($"api/product/image",content);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(
                await _httpClient.GetStreamAsync($"api/product"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<IEnumerable<Product>> GetProductsDefinitions()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(
                    await _httpClient.GetStreamAsync($"api/product/definitions"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetProductDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Product>(
              await _httpClient.GetStreamAsync($"api/product/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task<Product> GetProductDetailsByCode(string code)
        {
            code = code.Replace("/", "");

            return await JsonSerializer.DeserializeAsync<Product>(
              await _httpClient.GetStreamAsync($"api/product/GetProductDetailsByCode/{code}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveProduct(Product product)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(product),
              Encoding.UTF8, "application/json");

            if (product.id_producto == 0)
                await _httpClient.PostAsync("api/product", clientJson);
            else
                await _httpClient.PutAsync("api/product", clientJson);
        }
    }
}
