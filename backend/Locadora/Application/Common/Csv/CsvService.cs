using Application.Common.Exceptions;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Application.Common.Csv
{
    public class CsvService : ICsvService
    {
        public T ConvertCsvForRecord<T>(IFormFile arquivoCsv) where T : class
        {
            try
            {
                using (var stream = arquivoCsv.OpenReadStream())
                using (StreamReader sr = new StreamReader(stream))
                using (var csvReader = new CsvReader(sr, CultureInfo.CurrentCulture))
                {
                    return csvReader.GetRecord<T>();
                }
            } 
            catch
            {
                throw new InvalidCsvException();
            }
        }

        public List<T> ConvertCsvForRecords<T>(IFormFile arquivoCsv) where T : class
        {
            try
            {
                using (var stream = arquivoCsv.OpenReadStream())
                using (StreamReader sr = new StreamReader(stream))
                using (var csvReader = new CsvReader(sr, CultureInfo.CurrentCulture))
                {
                    return csvReader.GetRecords<T>().ToList();
                }
            }
            catch
            {
                throw new InvalidCsvException();
            }
        }
    }
}
