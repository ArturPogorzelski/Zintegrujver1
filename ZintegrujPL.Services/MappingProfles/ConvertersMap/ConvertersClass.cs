using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.Infrastructure.ConvertersMap
{
    public class NullableFloatConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Jeśli wartość jest pusta lub nie da się przekonwertować na float, zwróć 0
            if (string.IsNullOrEmpty(text))
            {
                return 0f;  // Zwraca 0 dla pustej wartości
            }

            if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            // Jeśli konwersja się nie powiedzie, zwróć 0
            return 0f;
        }
    }
    public class NullableBoolConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Jeśli wartość jest pusta, traktuj jako false
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            // Konwertuj na liczbę, jeśli to możliwe
            if (int.TryParse(text, out int numericValue))
            {
                return numericValue == 1;
            }

            // Możesz tutaj rzucić wyjątek lub zwrócić domyślną wartość (false)
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
    public class NullableIntConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0; // Zwraca 0 dla pustej wartości
            }

            if (int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }

            // Jeśli konwersja się nie powiedzie, zwróć 0
            return 0;
        }
    }
    public class NullableDecimalConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0m; // Zwraca 0 dla pustej wartości
            }

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            // Jeśli konwersja się nie powiedzie, zwróć 0
            return 0m;
        }
    }

    public class IgnoreQuotesStringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Jeśli tekst jest null lub pusty, zwróć pusty ciąg znaków
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            // Usuń wszystkie wystąpienia cudzysłowów z tekstowego ciągu danych
            var processedText = text.Replace("\"", "");

            // Jeśli po usunięciu cudzysłowów tekst jest pusty, zwróć również pusty ciąg znaków
            return processedText;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            // Przy zapisie nie musimy nic robić specjalnego, ale możemy tu zaimplementować logikę, jeśli potrzebujemy
            return value?.ToString() ?? "";
        }
    }
    public class CustomFloatConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Usuń cudzysłowy i zignoruj puste wartości, zwracając null lub domyślną wartość
            text = text.Replace("\"", "").Trim();
            if (string.IsNullOrEmpty(text))
                return 0f; // lub null, jeśli kolumna może być null

            if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
                return result;

            throw new InvalidOperationException($"Nie można przekonwertować '{text}' na float.");
        }
    }



}
