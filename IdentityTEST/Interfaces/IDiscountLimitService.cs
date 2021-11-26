using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IDiscountLimitService
    {
        Task<IEnumerable<DiscountLimit>> GetAllDiscountLimits();

        Task<IEnumerable<DiscountLimit>>  GetDiscountLimitsByBudgetID(int budgetId);

        Task<DiscountLimit> GetDiscountLimitDetails(int id);

        Task SaveDiscountLimit(DiscountLimit discountLimit);


        Task DeleteDiscountLimit(int id);
    }
}
