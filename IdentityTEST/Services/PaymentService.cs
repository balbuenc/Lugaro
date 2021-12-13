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
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeletePayment(int id)
        {
            await _httpClient.DeleteAsync($"api/payment/{id}");
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Payment>>(
                await _httpClient.GetStreamAsync($"api/payment"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Payment> GetPaymentDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Payment>(
              await _httpClient.GetStreamAsync($"api/payment/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SavePayment(Payment payment)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(payment),
              Encoding.UTF8, "application/json");

            if (payment.id_pago == 0)
                await _httpClient.PostAsync("api/payment", clientJson);
            else
                await _httpClient.PutAsync("api/payment", clientJson);
        }
    }
}
