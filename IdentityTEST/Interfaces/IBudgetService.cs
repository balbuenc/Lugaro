using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> GetAllBudgets();

        Task<IEnumerable<Budget>> GetAllBudgetsByUserName(string userName, bool canViewOnlyOwned);
        Task<IEnumerable<Budget>> GetAllApprovedBudgetsByUserName(string userName, bool canViewOnlyOwned);
        Task<IEnumerable<Budget>> GetAllApprovedBudgetsByClientID(string userName, bool canViewOnlyOwned, Int32 clienID, String condicionVenta, String motivo);

        Task<Budget> GetBudgetDetails(int id);

        Task<Int32> SaveBudget(Budget budget);


        Task<HttpResponseMessage> DeleteBudget(int id);
    }
}
