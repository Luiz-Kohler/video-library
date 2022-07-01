using MediatR;

namespace Application.Services.Clientes.Criar
{
    public class CriarClienteRequest : IRequest<CriarClienteResponse>
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
