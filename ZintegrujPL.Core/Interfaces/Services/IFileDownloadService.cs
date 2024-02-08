using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Core.Interfaces.Services
{
    public interface IFileDownloadService
    {
        Task<string> DownloadFileAsync(string fileUrl, string localFolderPath, string fileName);
    }
}
