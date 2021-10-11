using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrders();

        Task<PurchaseOrder> GetPurchaseOrderDetails(int id);

        Task<bool> InsertPurchaseOrder(PurchaseOrder purchaseOrder);

        Task<bool> UpdatePurchaseOrder(PurchaseOrder purchaseOrder);

        Task<bool> DeletePurchaseOrder(int id);
    }
}
