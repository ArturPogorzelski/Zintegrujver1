using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Core.Interfaces.Services
{
    public interface IFileProcessingService
    {
        Task ProcessProductsFileAsync(string filePath);
        Task ProcessInventoriesFileAsync(string filePath);
        Task ProcessPricesFileAsync(string filePath);
    }
}
