using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreERP.UI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<IEnumerable<Product>> GetProductsDefinitions();

        Task<Product> GetProductDetails(int id);
        Task<Product> GetProductDetailsByCode(string code);

        Task SaveProduct(Product area);


        Task DeleteProduct(int id);

        Task<HttpResponseMessage> SaveImage(MultipartFormDataContent content);
    }
}
