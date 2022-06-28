using MediatR;

namespace Application.Services.Locacoes.Atualizar
{
    public class AtualizarLocacaoRequest : IRequest<AtualizarLocacaoResponse>
    {
        public int LocacaoId { get; set; }
        public int FilmeId { get; set; }
        public int ClienteId { get; set; }
    }
}
