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
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task DeleteStock(int id)
        {
            await _httpClient.DeleteAsync($"api/stock/{id}");
        }

        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Stock>>(
                  await _httpClient.GetStreamAsync($"api/stock"),
                  new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                  );
        }

        public async Task<IEnumerable<Stock>> GetProductStock(int id)
        {
            try
            {
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<Stock>>(
                      await _httpClient.GetStreamAsync($"api/Stock/ProductStock/{id}"),
                      new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                      );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Stock>> GetTransferDestinations(int id)
        {
            try
            {
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<Stock>>(
                      await _httpClient.GetStreamAsync($"api/Stock/GetTransferDestinations/{id}"),
                      new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                      );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Stock> GetStockDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Stock>(
            await _httpClient.GetStreamAsync($"api/stock/{id}"),
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
            );
        }

        public async Task SaveStock(Stock stock)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(stock),
             Encoding.UTF8, "application/json");

            if (stock.id_stock == 0)
                await _httpClient.PostAsync("api/stock", clientJson);
            else
                await _httpClient.PutAsync("api/stock", clientJson);
        }
    }
}
