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
    public class LittleBoxDetailsService : ILittleBoxDetailsService
    {
        private readonly HttpClient _httpClient;

        public LittleBoxDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteLittleBoxDetail(int id)
        {
            await _httpClient.DeleteAsync($"api/LittleBoxDetails/{id}");
        }

        public async Task<IEnumerable<LittleBoxDetails>> GetAllLittleBoxDetail(int littelBoxID)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<LittleBoxDetails>>(
                await _httpClient.GetStreamAsync($"api/LittleBoxDetails/{littelBoxID}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<LittleBoxDetails> GetLittleBoxDetailsDetail(int id)
        {
            return await JsonSerializer.DeserializeAsync<LittleBoxDetails>(
              await _httpClient.GetStreamAsync($"api/LittleBoxDetails/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveLittleBoxDetail(LittleBoxDetails littleBoxDetail)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(littleBoxDetail),
              Encoding.UTF8, "application/json");

            if (littleBoxDetail.id_caja_chica_detalle == 0)
                await _httpClient.PostAsync("api/LittleBoxDetails", clientJson);
            else
                await _httpClient.PutAsync("api/LittleBoxDetails", clientJson);
        }

    }
}
