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
    public class TransferService : ITransferService
    {
        private readonly HttpClient _httpClient;

        public TransferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteTransfer(int id)
        {
            await _httpClient.DeleteAsync($"api/transfer/{id}");
        }

        public async Task<IEnumerable<Transfer>> GetAllTransfers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Transfer>>(
                await _httpClient.GetStreamAsync($"api/transfer"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Transfer> GetTransferDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Transfer>(
              await _httpClient.GetStreamAsync($"api/transfer/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveTransfer(Transfer transfer)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(transfer),
              Encoding.UTF8, "application/json");

            if (transfer.id_transferencia == 0)
                await _httpClient.PostAsync("api/transfer", clientJson);
            else
                await _httpClient.PutAsync("api/transfer", clientJson);
        }
    }
}
