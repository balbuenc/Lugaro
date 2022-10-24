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
    public class AccountPlanService : IAccountPlanService
    {
        private readonly HttpClient _httpClient;

        public AccountPlanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteAccountPlan(int id)
        {
            await _httpClient.DeleteAsync($"api/accountPlan/{id}");
        }

        public async Task<IEnumerable<AccountPlan>> GetAllAccountPlans()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<AccountPlan>>(
                await _httpClient.GetStreamAsync($"api/accountPlan"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<AccountPlan> GetAccountPlanDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<AccountPlan>(
              await _httpClient.GetStreamAsync($"api/accountPlan/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveAccountPlan(AccountPlan accountPlan)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(accountPlan),
              Encoding.UTF8, "application/json");

            if (accountPlan.id_plan_cuenta == 0)
                await _httpClient.PostAsync("api/accountPlan", clientJson);
            else
                await _httpClient.PutAsync("api/accountPlan", clientJson);
        }
    }
}
