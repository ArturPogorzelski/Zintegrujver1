using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models;

namespace ZintegrujPL.Core.Interfaces.Repositories
{
    
        public interface IPriceRepository
        {
        
            Task<IEnumerable<Price>> GetAllPricesAsync();
            Task<Price> GetPriceByIdAsync(int priceId);
            Task<IEnumerable<Price>> GetPricesByProductIdAsync(int productId);
            Task<Price> GetPriceBySKUAsync(string sku);
            Task AddPriceAsync(Price price);
            Task UpdatePriceAsync(Price price);
            Task DeletePriceAsync(int priceId);
           Task TruncatePriceAsync();
        Task AddPricesAsync(IEnumerable<Price> prices);
    }
   
}
