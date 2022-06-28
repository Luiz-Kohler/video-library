using MediatR;

namespace Application.Services.Locacoes.Buscar
{
    public class BuscarLocacaoRequest : IRequest<BuscarLocacaoResponse>
    {
        public int Id { get; set; }
    }
}
