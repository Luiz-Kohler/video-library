using MediatR;

namespace Application.Services.Clientes.Atualizar
{
    public class AtualizarClienteRequest : IRequest<AtualizarClienteResponse>
    {
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimento do cliente
        /// </summary>
        public DateTime DataNascimento { get; set; }
    }
}
