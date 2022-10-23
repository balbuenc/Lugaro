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

        public async Task<HttpResponseMessage> DeleteBudget(int id)
        {
           return  await _httpClient.DeleteAsync($"api/budget/{id}");
        }

        public async Task<IEnumerable<Budget>> GetAllBudgets()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Budget>>(
                 await _httpClient.GetStreamAsync($"api/budget"),
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                 );
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsByUserName(string userName, bool canViewOnlyOwned)
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

        public async Task<Int32> SaveBudget(Budget budget)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(budget),
               Encoding.UTF8, "application/json");

            String id;

            if (budget.id_presupuesto == 0)
            {
                var response = await _httpClient.PostAsync("api/budget", clientJson);
                id = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var response = await _httpClient.PutAsync("api/budget", clientJson);
                id = "0";
            }



            return Convert.ToInt32(id);
        }
    }
}
