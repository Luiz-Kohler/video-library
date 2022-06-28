using MediatR;

namespace Application.Services.Filmes.Excluir
{
    public class ExcluirFilmeRequest : IRequest<ExcluirFilmeResponse>
    {
        public int Id { get; set; }
    }
}
