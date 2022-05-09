using CoreERP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ICreditNoteDetailsService
    {
        Task<IEnumerable<CreditNoteDetails>> GetAllCreditNoteDetail(int CreditNoteID);

        Task<CreditNoteDetails> GetCreditNoteDetailsDetail(int id);

        Task SaveCreditNoteDetail(CreditNoteDetails creditNoteDetail);


        Task DeleteCreditNoteDetail(int id);
    }
}
