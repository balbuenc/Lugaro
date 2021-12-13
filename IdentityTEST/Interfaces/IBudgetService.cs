using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> GetAllBudgets();

        Task<IEnumerable<Budget>> GetAlGetAllBudgetsByUserName(string userName);

        Task<Budget> GetBudgetDetails(int id);

        Task SaveBudget(Budget budget);


        Task DeleteBudget(int id);
    }
}
