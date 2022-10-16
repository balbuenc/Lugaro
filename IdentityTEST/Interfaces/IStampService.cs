using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IStampService
    {
        Task<IEnumerable<Stamp>> GetAllStamps();

        Task<Stamp> GetStampDetails(int id);

        Task<Stamp> GetNextInvoiceNumber(int id);

        Task SaveStamp(Stamp stamp);


        Task DeleteStamp(int id);
    }
}
