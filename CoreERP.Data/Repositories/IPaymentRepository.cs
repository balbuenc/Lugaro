using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPayments();

        Task<Payment> GetPaymentDetails(int id);

        Task<bool> InsertPayment(Payment payment);

        Task<bool> UpdatePayment(Payment payment);

        Task<bool> DeletePayment(int id);
    }
}
