using MediatR;

namespace Application.Services.Clientes.Atualizar
{
    public class AtualizarClienteRequest : IRequest<AtualizarClienteResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
