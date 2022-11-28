using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllBudgets();
        Task<IEnumerable<Budget>> GetAllBudgetsByUserName(string userName, bool CanViewOnlyOwned);
        Task<IEnumerable<Budget>> GetAllApprovedBudgetsByUserName(string userName, bool canViewOnlyOwned);
        Task<IEnumerable<Budget>> GetAllApprovedBudgetsByClientID(string userName, bool canViewOnlyOwned, Int32 clienID, String condicionVenta, String motivo);

        Task<Budget> GetBudgetDetails(int id);

        Task<Int32> InsertBudget(Budget budget);

        Task<bool> UpdateBudget(Budget budget);

        Task<bool> DeleteBudget(int id);
    }
}
