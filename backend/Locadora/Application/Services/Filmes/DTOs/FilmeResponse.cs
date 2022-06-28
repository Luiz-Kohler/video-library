using Domain.Common.Enums;

namespace Application.Services.Filmes.DTOs
{
    public class FilmeResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Classificacao Classificacao { get; set; }
        public bool EhLancamento { get; set; }
    }
}
