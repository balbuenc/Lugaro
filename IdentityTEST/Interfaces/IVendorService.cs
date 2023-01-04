using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendors();
        Task<IEnumerable<Vendor>> GetVendorsDDL();

        Task<Vendor> GetVendorDetails(int id);

        Task SaveVendor(Vendor vendor);


        Task DeleteVendor(int id);
    }
}
