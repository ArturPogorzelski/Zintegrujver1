using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.DTOs;

namespace ZintegrujPL.Core.Interfaces.Services
{
  
        public interface IProductService
        {
            Task<IEnumerable<ProductDetailsDTO>> GetAllProductsAsync();
            Task<ProductDetailsDTO> GetProductDetailsBySKUAsync(string sku);
        }
    
}
