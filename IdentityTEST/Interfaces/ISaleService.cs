﻿using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSales();

        Task<Sale> GetSaleDetails(int id);

        Task<Sale> SaveSale(Budget budget);


        Task DeleteSale(int id);
    }
}
