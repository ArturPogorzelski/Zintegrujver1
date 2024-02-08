using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; } 
        public int ProductId { get; set; } 
        public string SKU { get; set; }
        public string Unit { get; set; }
        public int Qty { get; set; }
        public string Manufacturer { get; set; }
        public string Shipping { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
