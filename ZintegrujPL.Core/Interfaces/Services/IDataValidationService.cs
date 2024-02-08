using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models;

namespace ZintegrujPL.Core.Interfaces.Services
{
    public interface IDataValidationService
    {
        Task ValidateProductDataAsync(Product product);
        Task ValidateInventoryDataAsync(Inventory inventory);
        Task ValidatePriceDataAsync(Price price);
    }
}
