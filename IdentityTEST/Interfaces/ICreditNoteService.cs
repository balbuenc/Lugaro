using CoreERP.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ICreditNoteService
    {
        Task<IEnumerable<CreditNote>> GetAllCreditNotes();

        Task<CreditNote> GetCreditNoteDetails(int id);

        Task SaveCreditNote(CreditNote creditNote);


        Task DeleteCreditNote(int id);
    }
}
