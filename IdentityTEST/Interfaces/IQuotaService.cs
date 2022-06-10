using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IQuotaService
    {
        Task<IEnumerable<Quota>> GetAllQuotas();

        Task<Quota> GetQuotaDetails(int id);

        Task SaveQuota(Sale sale);

        Task UpdateQuota(Quota quota);


        Task DeleteQuota(int id);

        Task DeleteQuotaBySaleId(int id);
    }
}
