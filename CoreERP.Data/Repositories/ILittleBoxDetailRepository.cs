using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ILittleBoxDetailRepository
    {

        Task<IEnumerable<LittleBoxDetails>> GetAllLittleBoxDetails(int accountID);

        Task<LittleBoxDetails> GetLittleBoxDetailsDetail(int id);

        Task<bool> InsertLittleBoxDetail(LittleBoxDetails littleBoxDetails);

        Task<bool> UpdateLittleBoxDetail(LittleBoxDetails littleBoxDetails);

        Task<bool> DeleteLittleBoxDetail(int id);
    }
}
