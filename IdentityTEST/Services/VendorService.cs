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
    public class VendorService : IVendorService
    {
        private readonly HttpClient _httpClient;

        public VendorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteVendor(int id)
        {
            await _httpClient.DeleteAsync($"api/vendor/{id}");
        }

        public async Task<IEnumerable<Vendor>> GetAllVendors()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Vendor>>(
                await _httpClient.GetStreamAsync($"api/vendor"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<IEnumerable<Vendor>> GetVendorsDDL()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Vendor>>(
                await _httpClient.GetStreamAsync($"api/vendor/GetVendorsDDL"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                );
        }

        public async Task<Vendor> GetVendorDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Vendor>(
              await _httpClient.GetStreamAsync($"api/vendor/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }

        public async Task SaveVendor(Vendor vendor)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(vendor),
              Encoding.UTF8, "application/json");

            if (vendor.id_proveedor == 0)
                await _httpClient.PostAsync("api/vendor", clientJson);
            else
                await _httpClient.PutAsync("api/vendor", clientJson);
        }
    }
}
