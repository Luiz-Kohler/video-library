using Application.Services.Locacoes.DTOs;

namespace Application.Services.Locacoes.Listar
{
    public class ListarLocacoesResponse
    {
        public List<LocacaoResponse> Locacoes { get; set; }

        public ListarLocacoesResponse()
        {
            Locacoes = new List<LocacaoResponse>();
        }
    }
}