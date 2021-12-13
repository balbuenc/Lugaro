using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();

        Task<Payment> GetPaymentDetails(int id);
        Task<Payment> GetPaymentDetailsByPurchaseID(int id);
        Task<Payment> GetPaymentDetailsByGeneralPurchaseID(int id);


        Task SavePayment(Payment payment);


        Task DeletePayment(int id);
    }
}
