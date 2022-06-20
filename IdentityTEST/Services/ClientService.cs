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
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task DeleteClient(int id)
        {
            await _httpClient.DeleteAsync($"api/client/{id}");
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Client>>(
               await _httpClient.GetStreamAsync($"api/client"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
               );
        }


        public async Task<Client> GetDefaultClientDetails()
        {
            return await JsonSerializer.DeserializeAsync<Client>(
               await _httpClient.GetStreamAsync($"api/client/GetDefaultClientDetails"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
               );
        }

        public async Task<Client> GetClientDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Client>(
              await _httpClient.GetStreamAsync($"api/client/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveCient(Client client)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(client),
               Encoding.UTF8, "application/json");

            if (client.id_cliente == 0)
                await _httpClient.PostAsync("api/client", clientJson);
            else
                await _httpClient.PutAsync("api/client", clientJson);
        }
    }
}
