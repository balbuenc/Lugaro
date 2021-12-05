using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocks();

        Task<Stock> GetStockDetails(int id);
        Task<Stock> GetStockDetailsByStore(int ProductId, int StoreID);

        Task<bool> InsertStock(Stock store);

        Task<bool> UpdateStock(Stock store);

        Task<bool> DeleteStock(int id);

        Task<IEnumerable<Stock>> GetProductStock(int id);

        Task<IEnumerable<Stock>> GetTransferDestinations(int id);
    }
}
