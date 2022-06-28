using Domain.Common.Enums;
using MediatR;

namespace Application.Services.Filmes.Criar
{
    public class CriarFilmeRequest : IRequest<CriarFilmeResponse>
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Classificacao Classificacao { get; set; }
        public bool EhLancamento { get; set; }
    }
}
