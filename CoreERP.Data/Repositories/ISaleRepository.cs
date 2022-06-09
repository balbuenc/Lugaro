using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSales();

        Task<Sale> GetSaleDetails(int id);

        Task<Sale> GetSaleDetailsByInvoice(string invoice_number);

        Task<bool> InsertSale(Sale sale);

        Task<bool> UpdateSale(Sale sale);

        Task<bool> DeleteSale(int id);

        Task<Sale> GenerateSales(Budget budget);
    }
}
