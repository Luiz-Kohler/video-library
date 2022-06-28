using Application.Services.Filmes.DTOs;

namespace Application.Services.Filmes.Listar
{
    public class ListarFilmesResponse
    {
        public List<FilmeResponse> Filmes { get; set; }

        public ListarFilmesResponse()
        {
            Filmes = new List<FilmeResponse>();
        }
    }
}