using MediatR;

namespace Application.Services.Clientes.Criar
{
    public class CriarClienteRequest : IRequest<CriarClienteResponse>
    {
        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF do cliente (com ou sem mascara)
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Data de nascimento do cliente
        /// </summary>
        public DateTime DataNascimento { get; set; }
    }
}
