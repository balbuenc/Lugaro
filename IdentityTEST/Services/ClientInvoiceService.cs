using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class ClientInvoiceService : IClientInvoiceService
    {
        private readonly HttpClient _httpClient;

        public ClientInvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

     

        public async Task<IEnumerable<ClientInvoice>> GetAllClientInvoices()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ClientInvoice>>(
                await _httpClient.GetStreamAsync($"api/clientInvoice"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<IEnumerable<ClientInvoice>> GetClientInvoiceDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync <IEnumerable<ClientInvoice>>(
              await _httpClient.GetStreamAsync($"api/clientInvoice/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

       
    }
}
