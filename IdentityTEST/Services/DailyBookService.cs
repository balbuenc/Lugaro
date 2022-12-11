using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class DailyBookService : IDailyBookService
    {
        private readonly HttpClient _httpClient;

        public DailyBookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteDailyBook(int id)
        {
            await _httpClient.DeleteAsync($"api/DailyBook/{id}");
        }

        public async Task<IEnumerable<DailyBook>> GetAllDailyBooks()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<DailyBook>>(
                await _httpClient.GetStreamAsync($"api/DailyBook"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<DailyBook> GetDailyBookDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<DailyBook>(
              await _httpClient.GetStreamAsync($"api/DailyBook/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveDailyBook(DailyBook DailyBook)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(DailyBook),
              Encoding.UTF8, "application/json");

            if (DailyBook.id_plan_cuenta == 0)
                await _httpClient.PostAsync("api/DailyBook", clientJson);
            else
                await _httpClient.PutAsync("api/DailyBook", clientJson);
        }

        public async Task GenerateDailyBook(string factura)
        { 
                await _httpClient.PostAsync($"api/DailyBook/GenerateDailyBook/{factura}",null);
        }
    }
}
