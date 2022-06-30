using Application.Services.Clientes.DTOs;

namespace Application.Services.Clientes.Listar
{
    public class ListarClientesResponse
    {
        public List<ClienteResponse> Clientes { get; set; }

        public ListarClientesResponse()
        {
            Clientes = new List<ClienteResponse>();
        }
    }
}
