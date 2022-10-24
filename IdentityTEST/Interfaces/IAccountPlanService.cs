using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IAccountPlanService
    {
        Task<IEnumerable<AccountPlan>> GetAllAccountPlans();

        Task<AccountPlan> GetAccountPlanDetails(int id);

        Task SaveAccountPlan(AccountPlan accountPlan);


        Task DeleteAccountPlan(int id);
    }
}
