using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IDiscountLimitRepository
    {
        Task<IEnumerable<DiscountLimit>> GetAllDiscountLimits();

        Task<DiscountLimit> GetDiscountLimitDetails(int id);

        Task<bool> InsertDiscountLimit(DiscountLimit discountLimit);

        Task<bool> UpdateDiscountLimit(DiscountLimit discountLimit);

        Task<bool> DeleteDiscountLimit(int id);

        Task<IEnumerable<DiscountLimit>> GetDiscountLimitsByBudgetID(int budgetId);
    }
}
