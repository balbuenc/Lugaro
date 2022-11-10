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

        Task<Budget> GetBudgetDetails(int id);

        Task<Int32> InsertBudget(Budget budget);

        Task<bool> UpdateBudget(Budget budget);

        Task<bool> DeleteBudget(int id);
    }
}
