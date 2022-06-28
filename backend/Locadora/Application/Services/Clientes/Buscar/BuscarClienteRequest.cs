using MediatR;

namespace Application.Services.Clientes.Buscar
{
    public class BuscarClienteRequest : IRequest<BuscarClienteResponse>
    {
        public int Id { get; set; }
    }
}
