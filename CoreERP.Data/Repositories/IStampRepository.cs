using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IStampRepository
    {
        Task<IEnumerable<Stamp>> GetAllStamps();


        Task<Stamp> GetStampDetails(int id);

        Task<Stamp> GetNextInvoiceNumber(int id);

        Task<bool> InsertStamp(Stamp product);

        Task<bool> UpdateStamp(Stamp product);

        Task<bool> DeleteStamp(int id);
    }
}
