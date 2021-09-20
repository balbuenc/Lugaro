using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface ILittleBoxRepository
    {
        Task<IEnumerable<LittleBox>> GetAllLittleBox();

        Task<LittleBox> GetLittleBoxDetails(int id);

   

        Task<bool> InsertLittleBox(LittleBox littleBox);

        Task<bool> UpdateLittleBox(LittleBox littleBox);

        Task<bool> DeleteLittleBox(int id);
    }
}
