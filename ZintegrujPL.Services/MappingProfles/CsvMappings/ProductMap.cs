using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Infrastructure.ConvertersMap;
using ZintegrujPL.Models;

namespace ZintegrujPL.Infrastructure.Services.CsvMappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Map(m => m.ProductId).Ignore();
            Map(m => m.ID).Name("ID").TypeConverter<NullableIntConverter>();
            Map(m => m.SKU).Name("SKU");
            Map(m => m.Name).Name("name");
            Map(m => m.ReferenceNumber).Name("reference_number");
            Map(m => m.EAN).Name("EAN");
            Map(m => m.ProducerName).Name("producer_name");
            Map(m => m.Category).Name("category");
            Map(m => m.IsWire).Name("is_wire").TypeConverter<NullableBoolConverter>();
            Map(m => m.Available).Name("available").TypeConverter<NullableBoolConverter>();
            Map(m => m.DefaultImage).Name("default_image");
            Map(m => m.IsVendor).Name("is_vendor").TypeConverter<NullableBoolConverter>();
            Map(m => m.Shipping).Name("shipping");

            //Map(m => m.CanBeReturned).Name("can_be_returned").TypeConverter<NullableBoolConverter>();
            
            //Map(m => m.PackageSize).Name("package_size");
         
            //Map(m => m.LogisticHeight).Name("logistic_height").TypeConverter<NullableFloatConverter>();
            //Map(m => m.LogisticWidth).Name("logistic_width").TypeConverter<NullableFloatConverter>();
            //Map(m => m.LogisticLength).Name("logistic_length").TypeConverter<NullableFloatConverter>();
            //Map(m => m.LogisticWeight).Name("logistic_weight").TypeConverter<NullableFloatConverter>();
            
            //Map(m => m.AvailableInParcelLocker).Name("available_in_parcel_locker").TypeConverter<NullableBoolConverter>();
            
        }


    }

    
}
