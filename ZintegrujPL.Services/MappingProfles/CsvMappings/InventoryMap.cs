using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Infrastructure.ConvertersMap;
using ZintegrujPL.Models;

namespace ZintegrujPL.Infrastructure.Services.CsvMappings
{
    public class InventoryMap : ClassMap<Inventory>
    {
        public InventoryMap()
        {
            Map(m => m.InventoryId).Ignore();

            Map(m => m.ProductId)
                .Name("product_id")
                .TypeConverter<NullableIntConverter>()
                .Default(0); // Jeśli pole jest puste, użyj domyślnej wartości 0

            Map(m => m.SKU)
                .Name("sku")
                .TypeConverter<IgnoreQuotesStringConverter>(); // Usunie cudzysłowy z ciągu znaków

            Map(m => m.Unit)
                .Name("unit")
                .TypeConverter<IgnoreQuotesStringConverter>(); 

            Map(m => m.Qty)
                .Name("qty")
                .TypeConverter<NullableIntConverter>()
                .Default(0); 

            Map(m => m.Manufacturer)
                .Name("manufacturer_name")
                .TypeConverter<IgnoreQuotesStringConverter>(); 

            Map(m => m.Shipping)
                .Name("shipping")
                .TypeConverter<IgnoreQuotesStringConverter>();
            // Map(m => m.ShippingCost).Ignore();

            //Map(m => m.ShippingCost).Name("shipping_cost").TypeConverter<CustomFloatConverter>();
            Map(m => m.ShippingCost)
                .Name("shipping_cost")
                .TypeConverter<NullableDecimalConverter>()
                .Default(0);
        }
    }
}
