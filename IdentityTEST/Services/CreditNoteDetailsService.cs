using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class CreditNoteDetailsService : ICreditNoteDetailsService
    {
        private readonly HttpClient _httpClient;

        public CreditNoteDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteCreditNoteDetail(int id)
        {
            await _httpClient.DeleteAsync($"api/CreditNoteDetail/{id}");
        }

        public async Task<IEnumerable<CreditNoteDetails>> GetAllCreditNoteDetail(int CreditNoteID)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CreditNoteDetails>>(
                await _httpClient.GetStreamAsync($"api/CreditNoteDetail/{CreditNoteID}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<CreditNoteDetails> GetCreditNoteDetailsDetail(int id)
        {
            return await JsonSerializer.DeserializeAsync<CreditNoteDetails>(
              await _httpClient.GetStreamAsync($"api/CreditNoteDetail/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveCreditNoteDetail(CreditNoteDetails littleBoxDetail)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(littleBoxDetail),
              Encoding.UTF8, "application/json");

            if (littleBoxDetail.id_nota_credito_detalle == 0)
                await _httpClient.PostAsync("api/CreditNoteDetail", clientJson);
            else
                await _httpClient.PutAsync("api/CreditNoteDetail", clientJson);
        }
    }
}
