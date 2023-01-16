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
    public class StoreService : IStoreService
    {

        private readonly HttpClient _httpClient;

        public StoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteStore(int id)
        {
            await _httpClient.DeleteAsync($"api/store/{id}");
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Store>>(
                 await _httpClient.GetStreamAsync($"api/store"),
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                 );
        }

        public async Task<Store> GetStoreDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Store>(
             await _httpClient.GetStreamAsync($"api/store/{id}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
             );
        }

        public async Task<Store> GetStoreDetailsByName(string name)
        {
            return await JsonSerializer.DeserializeAsync<Store>(
             await _httpClient.GetStreamAsync($"api/store/GetStoreDetailsByName/{name}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
             );
        }

        public async Task SaveStore(Store store)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(store),
              Encoding.UTF8, "application/json");

            if (store.id_deposito == 0)
                await _httpClient.PostAsync("api/store", clientJson);
            else
                await _httpClient.PutAsync("api/store", clientJson);
        }
    }
}
