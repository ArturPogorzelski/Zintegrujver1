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
    public class PriceMap : ClassMap<Price>
    {
    
        public PriceMap()
        {
           
            Map(m => m.PriceId).Ignore();
           
            // pierwsza kolumna to unikatowy identyfikator magazynu zewnetrznego 
            Map(m => m.Column1).Index(0);
            // Druga kolumna to SKU - nie ma konieczności konwersji
            Map(m => m.Column2).Index(1);

            // Zakładamy, że trzecia kolumna to cena netto produktu
            Map(m => m.Column3).Index(2).TypeConverter<NullableDecimalConverter>();

            // Zakładamy, że czwarta kolumna to cena netto produktu po rabacie
            Map(m => m.Column4).Index(3).TypeConverter<NullableDecimalConverter>();

            // Zakładamy, że piąta kolumna to stawka VAT
            Map(m => m.Column5).Index(4).TypeConverter<NullableDecimalConverter>();

            // Zakładamy, że szósta kolumna to cena netto produktu po rabacie dla jednostki logistycznej produktu
            Map(m => m.Column6).Index(5).TypeConverter<NullableDecimalConverter>();

        }

    }
}
