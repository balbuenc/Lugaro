using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IAspNetUserRepository
    {
        Task<IEnumerable<AspNetUser>> GetAllAspNetUsers();

        Task<AspNetUser> GetAspNetUserDetails(int id);

        Task<bool> InsertAspNetUser(AspNetUser aspNetUser);

        Task<bool> UpdateAspNetUser(AspNetUser aspNetUser);

        Task<bool> DeleteAspNetUser(int id);
    }
}
