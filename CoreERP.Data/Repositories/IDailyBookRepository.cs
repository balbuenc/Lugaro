using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IDailyBookRepository
    {
        Task<IEnumerable<DailyBook>> GetAllDailyBooks();

        Task<DailyBook> GetDailyBookDetails(int id);

        Task<bool> InsertDailyBook(DailyBook DailyBook);

        Task<bool> GenerateDailyBook(string factura);

        Task<bool> UpdateDailyBook(DailyBook DailyBook);

        Task<bool> DeleteDailyBook(int id);
    }
}
