using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClients();

        Task<Client> GetClientDetails(int id);
        Task<Client> GetDefaultClientDetails();

        Task<bool> InsertClient(Client client);

        Task<bool> UpdateClient(Client client);

        Task<bool> DeleteClient(int id);
    }
}
