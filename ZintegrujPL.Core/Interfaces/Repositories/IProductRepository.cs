using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models;

namespace ZintegrujPL.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductBySKUAsync(string sku);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task TruncateProductsAsync();
        Task AddProductsAsync(IEnumerable<Product> products);
    }
}
