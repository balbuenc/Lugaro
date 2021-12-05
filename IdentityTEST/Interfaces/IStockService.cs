using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetAllStocks();

        Task<IEnumerable<Stock>> GetProductStock(int id);

        Task<IEnumerable<Stock>> GetTransferDestinations(int id);

        Task<Stock> GetStockDetails(int id);
        Task<Stock> GetStockDetailsByStore(int productId, int storeID);

        Task SaveStock(Stock stock);


        Task DeleteStock(int id);
    }
}
