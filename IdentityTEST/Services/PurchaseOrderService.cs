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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly HttpClient _httpClient;

        public PurchaseOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeletePurchaseOrder(int id)
        {
            await _httpClient.DeleteAsync($"api/purchaseOrder/{id}");
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrders()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<PurchaseOrder>>(
                await _httpClient.GetStreamAsync($"api/purchaseOrder"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<PurchaseOrder> GetPurchaseOrderDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<PurchaseOrder>(
              await _httpClient.GetStreamAsync($"api/purchaseOrder/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByPurchaseID(int id)
        {
            return await JsonSerializer.DeserializeAsync<PurchaseOrder>(
              await _httpClient.GetStreamAsync($"api/purchaseOrder/GetPurchaseOrderByPurchaseID/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SavePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(purchaseOrder),
              Encoding.UTF8, "application/json");

            if (purchaseOrder.id_orden_compra == 0)
                await _httpClient.PostAsync("api/purchaseOrder", clientJson);
            else
                await _httpClient.PutAsync("api/purchaseOrder", clientJson);
        }
    }
}
