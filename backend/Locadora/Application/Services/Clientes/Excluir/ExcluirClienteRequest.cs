using MediatR;

namespace Application.Services.Clientes.Excluir
{
    public class ExcluirClienteRequest : IRequest<ExcluirClienteResponse>
    {
        public int Id { get; set; }
    }
}
