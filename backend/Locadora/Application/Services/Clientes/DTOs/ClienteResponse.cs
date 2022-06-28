using Domain.Common.Enums;

namespace Application.Services.Clientes.DTOs
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusCliente Status { get; set; }
    }
}
