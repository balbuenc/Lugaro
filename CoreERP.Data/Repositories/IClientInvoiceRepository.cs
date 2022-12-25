using CoreERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IClientInvoiceRepository
    {
        Task<IEnumerable<ClientInvoice>> GetAllClientInvoices();

        Task<IEnumerable<ClientInvoice>> GetClientInvoiceDetails(int id);

     
    }
}
