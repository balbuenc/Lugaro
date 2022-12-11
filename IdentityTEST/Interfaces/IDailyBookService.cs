using CoreERP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IDailyBookService
    {
        Task<IEnumerable<DailyBook>> GetAllDailyBooks();

        Task<DailyBook> GetDailyBookDetails(int id);

        Task SaveDailyBook(DailyBook DailyBook);


        Task DeleteDailyBook(int id);
    }
}
