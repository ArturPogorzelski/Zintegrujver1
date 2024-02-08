using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.DAL.Database;
using ZintegrujPL.Models;

namespace ZintegrujPL.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public InventoryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Inventory>("SELECT * FROM Inventories");
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Inventory>(
                "SELECT * FROM Inventories WHERE InventoryId = @InventoryId", new { InventoryId = inventoryId });
        }

       

        public async Task AddInventoryAsync(Inventory inventory)
        {
            var query = @"
            INSERT INTO Inventories (ProductId, SKU, Unit, Qty, Manufacturer, Shipping, ShippingCost) 
            VALUES (@ProductId, @SKU, @Unit, @Qty, @Manufacturer, @Shipping, @ShippingCost)";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    inventory.ProductId,
                    inventory.SKU,
                    inventory.Unit,
                    inventory.Qty,
                    inventory.Manufacturer,
                    //inventory.ManufacturerRefNum,
                    inventory.Shipping,
                    inventory.ShippingCost
                });
            }
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            var query = @"
            UPDATE Inventories 
            SET 
                ProductId = @ProductId, 
                SKU = @SKU, 
                Unit = @Unit, 
                Qty = @Qty, 
                Manufacturer = @Manufacturer, 
                Shipping = @Shipping, 
                ShippingCost = @ShippingCost 
            WHERE 
                InventoryId = @InventoryId";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    inventory.ProductId,
                    inventory.SKU,
                    inventory.Unit,
                    inventory.Qty,
                    inventory.Manufacturer,
                    inventory.Shipping,
                    inventory.ShippingCost,
                    inventory.InventoryId
                });
            }
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Inventories WHERE InventoryId = @InventoryId", new { InventoryId = inventoryId });
        }
        
        public async Task<IEnumerable<Inventory>> GetInventoryByProductIdAsync(int productId)
        {
            var query = "SELECT * FROM Inventories WHERE ProductID = @ProductID";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var inventories = await connection.QueryAsync<Inventory>(query, new { ProductOutsideId = productId });
                return inventories;
            }
        }
        public async Task<Inventory> GetInventoryBySKUAsync(string sku)
        {
            // Przykładowa implementacja z użyciem Dappera
            using var connection = _dbConnectionFactory.CreateConnection();
            var query = "SELECT * FROM Inventories WHERE SKU = @SKU";
            return await connection.QuerySingleOrDefaultAsync<Inventory>(query, new { SKU = sku });
        }
        public async Task TruncateInventoryAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("TRUNCATE TABLE Inventories");
        }
        public async Task AddInventoriesAsync(IEnumerable<Inventory> inventories)
        {
            var query = @"
    INSERT INTO Inventories (ProductId, SKU, Unit, Qty, Manufacturer, Shipping, ShippingCost) 
    VALUES (@ProductId, @SKU, @Unit, @Qty, @Manufacturer, @Shipping, @ShippingCost)";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                foreach (var inventory in inventories)
                {
                    await connection.ExecuteAsync(query, new
                    {
                        inventory.ProductId,
                        inventory.SKU,
                        inventory.Unit,
                        inventory.Qty,
                        inventory.Manufacturer,
                        inventory.Shipping,
                        inventory.ShippingCost
                    });
                }
            }
        }
    }
}
