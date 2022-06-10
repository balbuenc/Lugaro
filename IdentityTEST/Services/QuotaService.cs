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
    public class QuotaService : IQuotaService
    {
        private readonly HttpClient _httpClient;

        public QuotaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteQuota(int id)
        {
            await _httpClient.DeleteAsync($"api/quota/{id}");
        }

        public async Task DeleteQuotaBySaleId(int id)
        {
            await _httpClient.DeleteAsync($"api/quota/DeleteQuotaBySaleId/{id}");
        }

        public async Task<IEnumerable<Quota>> GetAllQuotas()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Quota>>(
                await _httpClient.GetStreamAsync($"api/quota"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Quota> GetQuotaDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Quota>(
              await _httpClient.GetStreamAsync($"api/quota/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveQuota(Sale sale)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(sale),
              Encoding.UTF8, "application/json");

         
            await _httpClient.PostAsync("api/quota", clientJson);

        }

        public async Task UpdateQuota(Quota quota)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(quota),
              Encoding.UTF8, "application/json");


            await _httpClient.PutAsync("api/quota", clientJson);

        }

    }
}
