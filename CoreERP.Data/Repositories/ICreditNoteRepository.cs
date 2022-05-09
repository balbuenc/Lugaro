using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ICreditNoteRepository
    {
        Task<IEnumerable<CreditNote>> GetAllCreditNotes();

        Task<CreditNote> GetCreditNoteDetails(int id);

        Task<bool> InsertCreditNote(CreditNote credtiNote);

        Task<bool> UpdateCreditNote(CreditNote creditNote);

        Task<bool> DeleteCreditNote(int id);
    }
}
