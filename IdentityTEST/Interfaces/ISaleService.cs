using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSales();

        Task<IEnumerable<Sale>> GetSalesByUserName(string userName, bool canViewOnlyOwned);

        Task<IEnumerable<Sale>> GetInvoiceNumbers();

        Task<Sale> GetSaleDetails(int id);

        Task<Sale> GetSaleDetailsByInvoice(string invoice_number);


        Task<Sale> SaveSale(Budget budget);

        Task UpdateSale(Sale sale); 


        Task DeleteSale(int id);
    }
}
