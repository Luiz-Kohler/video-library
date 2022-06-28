using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Filmes.Importar
{
    public class ImportarFilmesRequest : IRequest<ImportarFilmesResponse>
    {
        public IFormFile ArquivoCsv { get; set; }
    }
}
