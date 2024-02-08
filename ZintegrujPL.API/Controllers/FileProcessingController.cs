using Microsoft.AspNetCore.Mvc;
using ZintegrujPL.Core.Interfaces.Services;
using static System.Net.WebRequestMethods;
using ZintegrujPL.Models;

namespace ZintegrujPL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileProcessingController : ControllerBase
    {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly ILogger<FileProcessingController> _logger;

        public FileProcessingController(IFileProcessingService fileProcessingService, IFileDownloadService fileDownloadService, ILogger<FileProcessingController> logger)
        {
            _fileProcessingService = fileProcessingService;
            _fileDownloadService = fileDownloadService;
            _logger = logger;
        }

        [HttpPost("ProcessFiles")]
        public async Task<IActionResult> ProcessFiles(string productsUrl, string inventoryUrl, string pricesUrl, string localFolderPath)
        {
           
           // productsUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
          // inventoryUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
           // pricesUrl = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";
           // localFolderPath = "Pliki";

            try
            {
                _logger.LogInformation("File processing started.");

                // Pobieranie i przetwarzanie pliku Products.csv
                
                var productsFilePath = await _fileDownloadService.DownloadFileAsync(productsUrl, localFolderPath, "Products.csv");
                await _fileProcessingService.ProcessProductsFileAsync(productsFilePath);
               
                // Pobieranie i przetwarzanie pliku Inventory.csv
                var inventoryFilePath = await _fileDownloadService.DownloadFileAsync(inventoryUrl, localFolderPath, "Inventory.csv");
                await _fileProcessingService.ProcessInventoriesFileAsync(inventoryFilePath);
               
                // Pobieranie i przetwarzanie pliku Prices.csv
                var pricesFilePath = await _fileDownloadService.DownloadFileAsync(pricesUrl, localFolderPath, "Prices.csv");
                await _fileProcessingService.ProcessPricesFileAsync(pricesFilePath);
                
                _logger.LogInformation("File processing completed successfully.");
                return Ok("Files processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during file processing.");
                return StatusCode(500, "An error occurred while processing the files.");
            }
        }
    }
}
