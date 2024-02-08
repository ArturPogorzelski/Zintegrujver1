using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.Core.Interfaces.Services;
using ZintegrujPL.Infrastructure.Services.CsvMappings;
using ZintegrujPL.Models;

namespace ZintegrujPL.Infrastructure.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly ILogger<FileProcessingService> _logger;
        private IProductRepository @object;

        public FileProcessingService(
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IPriceRepository priceRepository,
            IFileDownloadService fileDownloadService,
            ILogger<FileProcessingService> logger)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _priceRepository = priceRepository;
            _fileDownloadService = fileDownloadService;
            _logger = logger;
        }

        public FileProcessingService(IProductRepository @object)
        {
            this.@object = @object;
        }

        // Przetwarza plik Products.csv
        public async Task ProcessProductsFileAsyncOLD(string filePath)
        {
            await _productRepository.TruncateProductsAsync();
            //await _inventoryRepository.TruncateInventoryAsync();
            //await _priceRepository.TruncatePriceAsync();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", // Ustaw delimiter na średnik
                //HasHeaderRecord = true, // Plik zawiera nagłówki kolumn
                //IgnoreBlankLines = true,
                //MissingFieldFound = null,
            }))
            {
                csv.Context.RegisterClassMap<ProductMap>();
                var records = csv.GetRecords<Product>().ToList();


                if (records.Any())
                {
                    try
                    {
                        var record = csv.GetRecord<Product>();
                        if (record.IsWire == false && record.Shipping == "24h")
                        {
                            await _productRepository.AddProductAsync(record);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing record at row {csv.Context.Parser.Row}.");
                        var temp = "";

                    }
                    await _productRepository.AddProductsAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }
                //foreach (var record in records)
                //{
                //    if (record.IsWire == false && record.Shipping == "24h")  // Filtruj produkty
                //    {
                //        await _productRepository.AddProductAsync(record);
                //    }
                //}
            }
        }

        public async Task ProcessProductsFileAsync(string filePath)
        {

            await _productRepository.TruncateProductsAsync();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                //HasHeaderRecord = true,
                //IgnoreBlankLines = true,
                //MissingFieldFound = null,
            }))
            {
                csv.Context.RegisterClassMap<ProductMap>();
                var records = new List<Product>();
                while (csv.Read())
                {
                    try
                    {
                        var record = csv.GetRecord<Product>();
                        if (record.IsWire == false && record.Shipping == "24h")
                        {
                            records.Add(record);
                            //await _productRepository.AddProductAsync(record);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing record at row {csv.Context.Parser.Row}.");
                        var temp = "";

                    }
                   
                }
                if (records.Any())
                {
                    await _productRepository.AddProductsAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }
            }
        }
        // Przetwarza plik Inventory.csv
        public async Task ProcessInventoriesFileAsyncOLD2(string filePath)
        {

            await _inventoryRepository.TruncateInventoryAsync();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            }))
            {
                csv.Context.RegisterClassMap<InventoryMap>();
                var records = csv.GetRecords<Inventory>().ToList();

                if (records.Any())
                {
                    await _inventoryRepository.AddInventoriesAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }

                //foreach (var record in records)
                //{

                //    await _inventoryRepository.AddInventoryAsync(record);

                //}
            }
        }

        
        public async Task ProcessInventoriesFileAsync(string filePath)
        {
            await _inventoryRepository.TruncateInventoryAsync();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            }))
            {
                csv.Context.RegisterClassMap<InventoryMap>();
                var records = new List<Inventory>();
                while (csv.Read())
                {
                    try
                    {
                        var record = csv.GetRecord<Inventory>();
                        if (record.Shipping == "24h")
                        {
                            records.Add(record);
                            //await _productRepository.AddProductAsync(record);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing record at row {csv.Context.Parser.Row}.");
                        var testc = "";
                        
                    }
                }

                if (records.Any())
                {
                    await _inventoryRepository.AddInventoriesAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }
            }
        }

        public async Task ProcessInventoriesFileAsyncOLD(string filePath)
        {
            await _inventoryRepository.TruncateInventoryAsync();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {

                // Definiuje znak, który powinien być używany jako separator pola (np. przecinek)
                Delimiter = ",",
                //// Określa, czy rekordy powinny być ignorowane, jeśli są puste
                //IgnoreBlankLines = true,
                //// Określa, że wiersze zawierające błędne dane powinny być ignorowane
                //BadDataFound = null,
                //// Określa, jak powinny być traktowane pola, których brakuje
                //MissingFieldFound = null,


            }))
            {
                csv.Context.RegisterClassMap<InventoryMap>();

                var records = new List<Inventory>();
                while (csv.Read())
                {
                    try
                    {
                        var record = csv.GetRecord<Inventory>();
                        records.Add(record); // Dodawanie rekordów do listy zamiast bezpośredniego zapisu
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing record at row {csv.Context.Parser.Row}.");
                        // Możesz tutaj dodać bardziej szczegółowe logowanie błędów.
                    }
                }

                // Tutaj wykonaj zbiorcze dodanie wszystkich przetworzonych rekordów.
                if (records.Any())
                {
                    await _inventoryRepository.AddInventoriesAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }
            }
        }
        
        // Przetwarza plik Prices.csv
        public async Task ProcessPricesFileAsyncOLD(string filePath)
        {
            await _priceRepository.TruncatePriceAsync();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false, // Plik nie zawiera nagłówków kolumn
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            }))
            {
         
                csv.Context.RegisterClassMap<PriceMap>();
                var records = csv.GetRecords<Price>().ToList();

                if (records.Any())
                {
                    await _priceRepository.AddPricesAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }
                //foreach (var record in records)
                //{

                //    await _priceRepository.AddPriceAsync(record);

                //}

            }
        }
        public async Task ProcessPricesFileAsync(string filePath)
        {
            await _priceRepository.TruncatePriceAsync();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false, // Plik nie zawiera nagłówków kolumn
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            }))
            {
                csv.Context.RegisterClassMap<PriceMap>();
                var records = new List<Price>();
                while (csv.Read())
                {
                    try
                    {
                        var record = csv.GetRecord<Price>();
                       
                            records.Add(record);
                       
                        //await _priceRepository.AddPriceAsync(record);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing record at row {csv.Context.Parser.Row}.");
                        var testc = "";
                        
                    }
                }
                if (records.Any())
                {
                    await _priceRepository.AddPricesAsync(records); // Zmodyfikuj repozytorium, aby obsługiwało zbiorcze dodawanie.
                }

            }
        }
    }
}
