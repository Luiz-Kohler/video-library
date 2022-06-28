using MediatR;

namespace Application.Services.Locacoes.Criar
{
    public class CriarLocacaoRequest : IRequest<CriarLocacaoResponse>
    {
        public int FilmeId { get; set; }
        public int ClienteId { get; set; }
    }
}
