using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ILittleBoxDetailsService
    {
        Task<IEnumerable<LittleBoxDetails>> GetAllLittleBoxDetail(int LittleBoxID);

        Task<LittleBoxDetails> GetLittleBoxDetailsDetail(int id);

        Task SaveLittleBoxDetail(LittleBoxDetails littleBoxDetail);


        Task DeleteLittleBoxDetail(int id);
    }
}
