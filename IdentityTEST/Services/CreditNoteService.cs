using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class CreditNoteService : ICreditNoteService
    {
        private readonly HttpClient _httpClient;

        public CreditNoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteCreditNote(int id)
        {
            await _httpClient.DeleteAsync($"api/creditNote/{id}");
        }

        public async Task<IEnumerable<CreditNote>> GetAllCreditNotes()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CreditNote>>(
                await _httpClient.GetStreamAsync($"api/creditNote"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<CreditNote> GetCreditNoteDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<CreditNote>(
              await _httpClient.GetStreamAsync($"api/creditNote/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveCreditNote(CreditNote creditNote)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(creditNote),
              Encoding.UTF8, "application/json");

            if (creditNote.id_nota_credito == 0)
                await _httpClient.PostAsync("api/creditNote", clientJson);
            else
                await _httpClient.PutAsync("api/creditNote", clientJson);
        }
    }
}
