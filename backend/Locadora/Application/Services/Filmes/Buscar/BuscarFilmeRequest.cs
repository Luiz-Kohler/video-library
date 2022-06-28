using MediatR;

namespace Application.Services.Filmes.Buscar
{
    public class BuscarFilmeRequest : IRequest<BuscarFilmeResponse>
    {
        public int Id { get; set; }
    }
}
