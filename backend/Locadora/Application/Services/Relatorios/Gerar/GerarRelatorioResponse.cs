using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Relatorios.Gerar
{
    public class GerarRelatorioResponse
    {
        public FileStreamResult Arquivo { get; set; }
    }
}