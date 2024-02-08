using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.DTOs
{
    public class ProductDetailsDTO
    {
        // Informacje z Product
        public string Name { get; set; }
        //public string SKU { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public string DefaultImage { get; set; }

        // Informacje z Inventory
        public int InventoryQty { get; set; }
        public string Unit { get; set; }// jednostka logistyczna produktu
        public decimal ShippingCost { get; set; }

        // Informacje z Price
        public decimal NetProductPrice { get; set; }
        //public decimal NetProductPriceAfterDiscount { get; set; }
        //public decimal VATRate { get; set; }
       
    }
}
