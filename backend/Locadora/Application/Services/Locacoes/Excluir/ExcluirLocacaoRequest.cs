using MediatR;

namespace Application.Services.Locacoes.Excluir
{
    public class ExcluirLocacaoRequest : IRequest<ExcluirLocacaoResponse>
    {
        public int Id { get; set; }
    }
}
