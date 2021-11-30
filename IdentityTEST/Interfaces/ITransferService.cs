using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ITransferService
    {
        Task<IEnumerable<Transfer>> GetAllTransfers();

        Task<Transfer> GetTransferDetails(int id);

        Task SaveTransfer(Transfer transfer);


        Task DeleteTransfer(int id);
    }
}
