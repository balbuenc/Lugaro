using CoreERP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IClientInvoiceService
    {
        Task<IEnumerable<ClientInvoice>> GetAllClientInvoices();

        Task<IEnumerable<ClientInvoice>> GetClientInvoiceDetails(int id);
    }
}
