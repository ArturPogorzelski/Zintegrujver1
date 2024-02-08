using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models;

namespace ZintegrujPL.Core.Interfaces.Repositories
{
   
        public interface IInventoryRepository
        {
            Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
            Task<Inventory> GetInventoryByIdAsync(int inventoryId);
            Task<IEnumerable<Inventory>> GetInventoryByProductIdAsync(int productId);
            Task AddInventoryAsync(Inventory inventory);
            Task UpdateInventoryAsync(Inventory inventory);
            Task DeleteInventoryAsync(int inventoryId);
            Task<Inventory> GetInventoryBySKUAsync(string sku);
        Task TruncateInventoryAsync();
        Task AddInventoriesAsync(IEnumerable<Inventory> inventories);
    }
   
}
