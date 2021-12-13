using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IBudgetDetailRepository
    {
        Task<IEnumerable<BudgetDetails>> GetAllBudgetDetails();
        

        Task<IEnumerable<BudgetDetails>> GetBudgetDetails(int id);

        Task<IEnumerable<BudgetDetails>> GetBudgetPDFDetails(int id);

        Task<bool> InsertBudgetDetail(BudgetDetails budgetDetail);

        Task<bool> UpdateBudgetDetail(BudgetDetails budgetDetail);

        Task<bool> UpdateUnitaryPriceBudgetDetail(BudgetDetails budgetDetail);

        Task<bool> DeleteBudgetDetail(int id);
    }
}
