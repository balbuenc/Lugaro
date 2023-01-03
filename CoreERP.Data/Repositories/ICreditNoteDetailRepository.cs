using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ICreditNoteDetailRepository
    {
        Task<IEnumerable<CreditNoteDetails>> GetAllCreditNoteDetails(int CreditNoteID);

     

        Task<bool> InsertCreditNoteDetails(CreditNoteDetails creditNoteDetail);

        Task<bool> UpdateCreditNoteDetails(CreditNoteDetails creditNoteDetail);

        Task<bool> DeleteCreditNoteDetails(int id);
    }
}
