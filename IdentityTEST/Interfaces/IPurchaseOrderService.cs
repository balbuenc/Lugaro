using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrders();

        Task<PurchaseOrder> GetPurchaseOrderDetails(int id);

        Task SavePurchaseOrder(PurchaseOrder account);


        Task DeletePurchaseOrder(int id);
    }
}
