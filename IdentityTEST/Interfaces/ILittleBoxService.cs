using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ILittleBoxService
    {
        Task<IEnumerable<LittleBox>> GetAllLittleBoxs();

        Task<LittleBox> GetLittleBoxDetails(int id);

        Task SaveLittleBox(LittleBox littleBox);


        Task DeleteLittleBox(int id);
    }
}
