using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendors();
        Task<IEnumerable<Vendor>> GetVendorsDDL();

        Task<Vendor> GetVendorDetails(int id);

        Task<bool> InsertVendor(Vendor vendor);

        Task<bool> UpdateVendor(Vendor vendor);

        Task<bool> DeleteVendor(int id);
    }
}
