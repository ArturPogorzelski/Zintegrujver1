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
    public class PriceRepository : IPriceRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PriceRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Price>> GetAllPricesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Price>("SELECT * FROM Prices");
        }

        public async Task<Price> GetPriceByIdAsync(int priceId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Price>(
                "SELECT * FROM Prices WHERE PriceId = @PriceId", new { PriceId = priceId });
        }

        public async Task<IEnumerable<Price>> GetPricesByProductIdAsync(int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Price>(
                "SELECT * FROM Prices WHERE ProductID = @ProductID", new { ProductID = productId });
        }

        public async Task AddPriceAsync(Price price)
        {
            var query = @"
            INSERT INTO Prices (Column1, Column2, Column3, Column4, Column5, Column6) 
            VALUES (@Column1, @Column2, @Column3, @Column4, @Column5, @Column6)";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    price.Column1,//Unikalny ID 
                    price.Column2,//SKU
                    price.Column3,//cena netto
                    price.Column4,//cena netto po rabacie
                    price.Column5,//vat
                    price.Column6 // cena netto lednoltki logistycznej produktu po rabacie
                });
            }
        }

        public async Task UpdatePriceAsync(Price price)
        {
            var query = @"
            UPDATE Prices 
            SET 
                Column1 = @Column1, 
                Column2 = @Column2, 
                Column3 = @Column3, 
                Column4 = @Column4, 
                VATRate = @Column5, 
                Column6 = @Column6 
            WHERE 
                PriceId = @PriceId";
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    price.Column1,//Unikalny ID 
                    price.Column2,//SKU
                    price.Column3,//cena netto
                    price.Column4,//cena netto po rabacie
                    price.Column5,//vat
                    price.Column6, // cena netto lednoltki logistycznej produktu po rabacie
                    price.PriceId
                });
            }
        }

        public async Task DeletePriceAsync(int priceId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("DELETE FROM Prices WHERE PriceId = @PriceId", new { PriceId = priceId });
        }
        public async Task<Price> GetPriceBySKUAsync(string sku)
        {
            // Przykładowa implementacja z użyciem Dappera
            using var connection = _dbConnectionFactory.CreateConnection();
            var query = "SELECT * FROM Prices WHERE Column2 = @Column2";
            return await connection.QuerySingleOrDefaultAsync<Price>(query, new { Column2 = sku });
        }
        public async Task TruncatePriceAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync("TRUNCATE TABLE Prices");
        }
        public async Task AddPricesAsync(IEnumerable<Price> prices)
        {
            var query = @"
    INSERT INTO Prices (Column1, Column2, Column3, Column4, Column5, Column6) 
    VALUES (@Column1, @Column2, @Column3, @Column4, @Column5, @Column6)";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                foreach (var price in prices)
                {
                    await connection.ExecuteAsync(query, new
                    {
                        price.Column1,
                        price.Column2,
                        price.Column3,
                        price.Column4,
                        price.Column5,
                        price.Column6
                    });
                }
            }
        }
    }
}
