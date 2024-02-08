using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Models
{
    public class Product
    {
        public int ProductId { get; set; } 
        public int ID { get; set; } 
        public string SKU { get; set; }
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public bool IsWire { get; set; } 
        public bool Available { get; set; }
        public bool IsVendor { get; set; }
        public string DefaultImage { get; set; }
        public string Shipping { get; set; }



    }
}
