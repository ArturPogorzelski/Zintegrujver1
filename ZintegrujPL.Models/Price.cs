using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public string Column1 { get; set; }//Unique ID, only used by internal warehouse system.
        public string Column2 { get; set; }//Product SKU, unique value created by warehouse
        public decimal Column3 { get; set; }//Nett product price
        public decimal Column4 { get; set; }//Nett product price after discount
        public decimal Column5 { get; set; }//VAT rate
        public decimal Column6 { get; set; }//Nett product price after discount for product logistic unit
    }
}
