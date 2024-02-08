using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.Models;
using Dapper;
using ZintegrujPL.DAL.Database;
using System.Data;
namespace ZintegrujPL.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
            return products.ToList();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var product = await connection.QuerySingleOrDefaultAsync<Product>(
                "SELECT * FROM Products WHERE ProductId = @ProductId", new { ProductId = productId });
            return product;
        }

        public async Task<Product> GetProductBySKUAsync(string sku)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var product = await connection.QuerySingleOrDefaultAsync<Product>(
                "SELECT * FROM Products WHERE SKU = @SKU", new { SKU = sku });
            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            var query = @"
            INSERT INTO Products (ID,SKU, Name, ReferenceNumber, EAN, ProducerName, Category, IsWire,Available,IsVendor,DefaultImage, Shipping)
            VALUES (@ID,@SKU, @Name, @ReferenceNumber, @EAN, @ProducerName, @Category, @IsWire,@Available,@IsVendor,@DefaultImage, @Shipping)";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    product.ID,
                    product.SKU,
                    product.Name,
                    product.ReferenceNumber,
                    product.EAN,
                    product.ProducerName,
                    product.Category,
                    product.IsWire,
                    product.Available,
                    product.IsVendor,
                    product.DefaultImage,
                    product.Shipping
                    
                    
                });
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            var query = @"
            UPDATE Products 
            SET 
                ID = @ID
                SKU = @SKU, 
                Name = @Name, 
                ReferenceNumber = @ReferenceNumber, 
                EAN = @EAN, 
                ProducerName = @ProducerName, 
                Category = @Category, 
                IsWire = @IsWire, 
                Available = @Available, 
                IsVendor = @IsVendor, 
                DefaultImage = @DefaultImage,
                Shipping = @Shipping
            WHERE 
                ProductId = @ProductId";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    product.ID,
                    product.SKU,
                    product.Name,
                    product.ReferenceNumber,
                    product.EAN,
                    product.ProducerName,
                    product.Category,
                    product.IsWire,
                    product.Available,
                    product.IsVendor,
                    product.DefaultImage,
                    product.Shipping,
                    product.ProductId 
                    //product.CanBeReturned,
                    //product.PackageSize,

                    //product.LogisticHeight,
                    //product.LogisticWidth,
                    //product.LogisticLength,
                    //product.LogisticWeight,

                    //product.AvailableInParcelLocker,

                });
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Products WHERE ProductId = @ProductId", new { ProductId = productId });
        }
        public async Task TruncateProductsAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("TRUNCATE TABLE Products");
        }
        public async Task AddProductsAsync(IEnumerable<Product> products)
        {
            var query = @"
    INSERT INTO Products (ID, SKU, Name, ReferenceNumber, EAN, ProducerName, Category, IsWire, Available, IsVendor, DefaultImage, Shipping)
    VALUES (@ID, @SKU, @Name, @ReferenceNumber, @EAN, @ProducerName, @Category, @IsWire, @Available, @IsVendor, @DefaultImage, @Shipping)";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                foreach (var product in products)
                {
                    await connection.ExecuteAsync(query, new
                    {
                        product.ID,
                        product.SKU,
                        product.Name,
                        product.ReferenceNumber,
                        product.EAN,
                        product.ProducerName,
                        product.Category,
                        product.IsWire,
                        product.Available,
                        product.IsVendor,
                        product.DefaultImage,
                        product.Shipping
                    });
                }
            }
        }
    }
}
