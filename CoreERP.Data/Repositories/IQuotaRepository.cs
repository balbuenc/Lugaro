using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IQuotaRepository
    {
        Task<IEnumerable<Quota>> GetAllQuotas();

        Task<Quota> GetQuotaDetails(int id);

        Task<bool> InsertQuota(Quota client);

        Task<bool> UpdateQuota(Quota client);

        Task<bool> DeleteQuota(int id);
        Task<bool> DeleteQuotaBySaleId(int id);

        Task<bool> GenerateSalesQuote(Sale sale);

    }
}
