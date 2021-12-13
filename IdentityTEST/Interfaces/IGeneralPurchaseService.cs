using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IGeneralPurchaseService
    {
        Task<IEnumerable<GeneralPurchase>> GetAllGeneralPurchases();

        Task<GeneralPurchase> GetGeneralPurchaseDetails(int id);

        Task SaveGeneralPurchase(GeneralPurchase purchase);


        Task DeleteGeneralPurchase(int id);
    }
}
