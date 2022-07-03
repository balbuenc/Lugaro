using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClients();

        Task<Client> GetClientDetails(int id);
        Task<Client> GetDefaultClientDetails();

        Task<Int32> SaveClient(Client client);


        Task DeleteClient(int id);
    }
}
