using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IGeneralPurchaseDetailsRepository
    {
        Task<IEnumerable<GeneralPurchaseDetails>> GetAllGeneralPurchaseDetails();

        Task<IEnumerable<GeneralPurchaseDetails>> GetGeneralPurchaseDetails(int id);

        Task<bool> InsertPurchaseDetail(GeneralPurchaseDetails purchaseDetails);

        Task<bool> UpdatePurchaseDetail(GeneralPurchaseDetails purchaseDetails);

        Task<bool> DeletePurchaseDetail(int id);
    }
}
