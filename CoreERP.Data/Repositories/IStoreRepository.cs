using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllStores();

        Task<Store> GetStoreDetails(int id);
        Task<Store> GetStoreDetailsByName(string name);

        Task<bool> InsertStore(Store store);

        Task<bool> UpdateStore(Store store);

        Task<bool> DeleteStore(int id);
    }
}
