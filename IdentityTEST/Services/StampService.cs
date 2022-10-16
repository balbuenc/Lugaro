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
    public class StampService : IStampService
    {

        private readonly HttpClient _httpClient;

        public StampService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteStamp(int id)
        {
            await _httpClient.DeleteAsync($"api/stamp/{id}");
        }

        public async Task<IEnumerable<Stamp>> GetAllStamps()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Stamp>>(
                await _httpClient.GetStreamAsync($"api/stamp"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Stamp> GetStampDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Stamp>(
              await _httpClient.GetStreamAsync($"api/stamp/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task<Stamp> GetNextInvoiceNumber(int id)
        {
            return await JsonSerializer.DeserializeAsync<Stamp>(
              await _httpClient.GetStreamAsync($"api/stamp/GetNextInvoiceNumber/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveStamp(Stamp stamp)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(stamp),
              Encoding.UTF8, "application/json");

            if (stamp.id_timbrado == 0)
                await _httpClient.PostAsync("api/stamp", clientJson);
            else
                await _httpClient.PutAsync("api/stamp", clientJson);
        }
    }
}
