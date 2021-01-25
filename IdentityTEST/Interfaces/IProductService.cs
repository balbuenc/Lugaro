﻿using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductDetails(int id);

        Task SaveProduct(Product area);


        Task DeleteProduct(int id);
    }
}