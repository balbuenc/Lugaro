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
    public class DiscountLimitService : IDiscountLimitService
    {
        private readonly HttpClient _httpClient;

        public DiscountLimitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteDiscountLimit(int id)
        {
            await _httpClient.DeleteAsync($"api/discountLimit/{id}");
        }

        public async Task<IEnumerable<DiscountLimit>> GetAllDiscountLimits()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<DiscountLimit>>(
                await _httpClient.GetStreamAsync($"api/discountLimit"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<IEnumerable<DiscountLimit>> GetDiscountLimitsByBudgetID(int budgetId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<DiscountLimit>>(
                await _httpClient.GetStreamAsync($"api/discountLimit/GetDiscountLimitsByBudgetID/{budgetId}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<DiscountLimit> GetDiscountLimitDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<DiscountLimit>(
              await _httpClient.GetStreamAsync($"api/discountLimit/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveDiscountLimit(DiscountLimit discountLimit)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(discountLimit),
              Encoding.UTF8, "application/json");

            if (discountLimit.id_limite_descuento == 0)
                await _httpClient.PostAsync("api/discountLimit", clientJson);
            else
                await _httpClient.PutAsync("api/discountLimit", clientJson);
        }
    }
}
