using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IGeneralPurchaseDetailsService
    {
        Task<IEnumerable<GeneralPurchaseDetails>> GetAllGeneralPurchaseDetails();

        Task<IEnumerable<GeneralPurchaseDetails>> GetGeneralPurchaseDetails(int id);

        Task<byte[]> GetPurchasePDF(int id);

        Task<byte[]> GetInvoicePDF(int id);

        Task SaveGeneralPurchaseDetails(GeneralPurchaseDetails budget);


        Task DeleteGeneralPurchaseDetails(int id);
    }
}
