using Microsoft.AspNetCore.Http;

namespace Application.Common.Csv
{
    public interface ICsvService
    {
        T ConvertCsvForRecord<T>(IFormFile arquivoCsv) where T : class;
        List<T> ConvertCsvForRecords<T>(IFormFile arquivoCsv) where T : class;
    }
}
