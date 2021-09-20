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
    public class LittleBoxService : ILittleBoxService
    {
        private readonly HttpClient _httpClient;

        public LittleBoxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteLittleBox(int id)
        {
            await _httpClient.DeleteAsync($"api/littleBox/{id}");
        }

        public async Task<IEnumerable<LittleBox>> GetAllLittleBoxs()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<LittleBox>>(
                await _httpClient.GetStreamAsync($"api/littleBox"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<LittleBox> GetLittleBoxDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<LittleBox>(
              await _httpClient.GetStreamAsync($"api/littleBox/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveLittleBox(LittleBox littleBox)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(littleBox),
              Encoding.UTF8, "application/json");

            if (littleBox.id_caja_chica == 0)
                await _httpClient.PostAsync("api/littleBox", clientJson);
            else
                await _httpClient.PutAsync("api/littleBox", clientJson);
        }
    }
}
