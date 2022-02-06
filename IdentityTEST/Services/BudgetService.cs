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
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _httpClient;

        public BudgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteBudget(int id)
        {
            await _httpClient.DeleteAsync($"api/budget/{id}");
        }

        public async Task<IEnumerable<Budget>> GetAllBudgets()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Budget>>(
                 await _httpClient.GetStreamAsync($"api/budget"),
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                 );
        }

        public async Task<IEnumerable<Budget>> GetAlGetAllBudgetsByUserName(string userName, bool canViewOnlyOwned)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Budget>>(
                 await _httpClient.GetStreamAsync($"api/budget/GetAllBudgetsByUserName/{userName}/{canViewOnlyOwned}"),
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                 );
        }

        public async Task<Budget> GetBudgetDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Budget>(
             await _httpClient.GetStreamAsync($"api/budget/{id}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
             );
        }

        public async Task SaveBudget(Budget budget)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(budget),
               Encoding.UTF8, "application/json");

            if (budget.id_presupuesto == 0)
                await _httpClient.PostAsync("api/budget", clientJson);
            else
                await _httpClient.PutAsync("api/budget", clientJson);
        }
    }
}
