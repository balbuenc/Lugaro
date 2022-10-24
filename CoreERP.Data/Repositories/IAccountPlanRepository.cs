using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IAccountPlanRepository
    {
        Task<IEnumerable<AccountPlan>> GetAllAccountPlans();

        Task<AccountPlan> GetAccountPlanDetails(int id);

        Task<bool> InsertAccountPlan(AccountPlan accountPlan);

        Task<bool> UpdateAccountPlan(AccountPlan accountPlan);

        Task<bool> DeleteAccountPlan(int id);
    }
}
