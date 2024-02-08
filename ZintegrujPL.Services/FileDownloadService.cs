using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Services;
using ZintegrujPL.Services;

namespace ZintegrujPL.Infrastructure.Services
{
    public class FileDownloadService : IFileDownloadService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FileDownloadService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Pobiera plik i zapisuje lokalnie
        public async Task<string> DownloadFileAsync(string fileUrl, string localFolderPath, string fileName)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(fileUrl);
            response.EnsureSuccessStatusCode();
            var fileContent = await response.Content.ReadAsByteArrayAsync();  // Pobieranie zawartości pliku jako byte array

            // Sprawdzenie, czy folder docelowy istnieje, jeśli nie - utworzenie
            if (!Directory.Exists(localFolderPath))
            {
                Directory.CreateDirectory(localFolderPath);
            }
            
            var localFilePath = Path.Combine(localFolderPath, fileName);
            await File.WriteAllBytesAsync(localFilePath, fileContent);  // Zapisywanie pliku na dysku

            // Konwersja ścieżki na pełną ścieżkę
            var fullLocalFilePath = Path.GetFullPath(localFilePath);

            return fullLocalFilePath;  
        }
    }
}

