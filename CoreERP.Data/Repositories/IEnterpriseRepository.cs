using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IEnterpriseRepository
    {
        Task<IEnumerable<Enterprise>> GetAllEnterprises();

        Task<Enterprise> GetEnterpriseDetails(int id);

        Task<bool> InsertEnterprise(Enterprise enterprise);

        Task<bool> UpdateEnterprise(Enterprise enterprise);

        Task<bool> DeleteEnterprise(int id);
    }
}
