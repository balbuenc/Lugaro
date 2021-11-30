using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ITransferRepository
    {
        Task<IEnumerable<Transfer>> GetAllTransfers();

        Task<Transfer> GetTransferDetails(int id);

        Task<bool> InsertTransfer(Transfer transfer);

        Task<bool> UpdateTransfer(Transfer transfer);

        Task<bool> DeleteTransfer(int id);
    }
}
