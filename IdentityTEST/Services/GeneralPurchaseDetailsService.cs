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
    public class GeneralPurchaseDetailsService :IGeneralPurchaseDetailsService
    {

        private readonly HttpClient _httpClient;
        public GeneralPurchaseDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteGeneralPurchaseDetails(int id)
        {
            await _httpClient.DeleteAsync($"api/GeneralPurchaseDetail/{id}");
        }

        public async Task<IEnumerable<GeneralPurchaseDetails>> GetAllGeneralPurchaseDetails()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GeneralPurchaseDetails>>(
                 await _httpClient.GetStreamAsync($"api/GeneralPurchaseDetail"),
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                 );
        }

        public async Task<IEnumerable<GeneralPurchaseDetails>> GetGeneralPurchaseDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GeneralPurchaseDetails>>(
             await _httpClient.GetStreamAsync($"api/GeneralPurchaseDetail/{id}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
             );
        }

        public async Task<byte[]> GetPurchasePDF(int id)
        {

            var response = await _httpClient.GetByteArrayAsync($"api/Report/DownloadReport/{id}");
            return response;

        }

        public async Task<byte[]> GetInvoicePDF(int id)
        {

            var response = await _httpClient.GetByteArrayAsync($"api/InvoicePrint/DownloadInvoice/{id}");
            return response;

        }


        public async Task SaveGeneralPurchaseDetails(GeneralPurchaseDetails budgetDetails)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(budgetDetails),
               Encoding.UTF8, "application/json");

            if (budgetDetails.id_compra_general_detalle == 0)
                await _httpClient.PostAsync("api/GeneralPurchaseDetail", clientJson);
            else
                await _httpClient.PutAsync("api/GeneralPurchaseDetail", clientJson);
        }
    }
}
