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
    public class GeneralPurchaseService : IGeneralPurchaseService
    {

        private readonly HttpClient _httpClient;

        public GeneralPurchaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteGeneralPurchase(int id)
        {
            await _httpClient.DeleteAsync($"api/Generalpurchase/{id}");
        }

        public async Task<IEnumerable<GeneralPurchase>> GetAllGeneralPurchases()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GeneralPurchase>>(
                await _httpClient.GetStreamAsync($"api/Generalpurchase"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<GeneralPurchase> GetGeneralPurchaseDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<GeneralPurchase>(
              await _httpClient.GetStreamAsync($"api/Generalpurchase/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveGeneralPurchase(GeneralPurchase purchase)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(purchase),
              Encoding.UTF8, "application/json");

            if (purchase.id_compra_general == 0)
                await _httpClient.PostAsync("api/Generalpurchase", clientJson);
            else
                await _httpClient.PutAsync("api/Generalpurchase", clientJson);
        }
    }

}
