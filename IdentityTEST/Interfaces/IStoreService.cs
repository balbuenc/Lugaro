using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetAllStores();

        Task<Store> GetStoreDetails(int id);
        Task<Store> GetStoreDetailsByName(string name);


        Task SaveStore(Store store);


        Task DeleteStore(int id);
    }
}
