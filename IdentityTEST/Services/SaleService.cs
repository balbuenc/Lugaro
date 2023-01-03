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

    public class SaleService : ISaleService
    {
        private readonly HttpClient _httpClient;

        public SaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteSale(int id)
        {
            await _httpClient.DeleteAsync($"api/sale/{id}");
        }


        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Sale>>(
                await _httpClient.GetStreamAsync($"api/sale"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<IEnumerable<Sale>> GetInvoiceNumbers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Sale>>(
                await _httpClient.GetStreamAsync($"api/sale/GetInvoiceNumbers"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Sale> GetSaleDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Sale>(
              await _httpClient.GetStreamAsync($"api/sale/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task<Sale> GetSaleDetailsByInvoice(string invoice_number)
        {
            return await JsonSerializer.DeserializeAsync<Sale>(
              await _httpClient.GetStreamAsync($"api/sale/GetSaleByInvoice/{invoice_number}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task<Sale> SaveSale(Budget budget)
        {
            try
            { 
            var clientJson = new StringContent(JsonSerializer.Serialize(budget), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/sale", clientJson);


            return await JsonSerializer.DeserializeAsync<Sale>(
                await _httpClient.GetStreamAsync($"api/sale/{budget.id_presupuesto}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateSale(Sale sale)
        {
            try
            {
                var clientJson = new StringContent(JsonSerializer.Serialize(sale), Encoding.UTF8, "application/json");

                await _httpClient.PutAsync("api/sale", clientJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
