using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IGeneralPurchaseRepository
    {
        Task<IEnumerable<GeneralPurchase>> GetAllGeneralPurchases();

        Task<GeneralPurchase> GetGeneralPurchaseDetails(int id);

        Task<bool> InsertGeneralPurchase(GeneralPurchase generalPurchase);

        Task<bool> UpdateGeneralPurchase(GeneralPurchase generalPurchase);

        Task<bool> DeleteGeneralPurchase(int id);
    }
}
