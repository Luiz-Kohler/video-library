using Domain.Common.Enums;

namespace Application.Services.Locacoes.DTOs
{
    public class LocacaoResponse
    {
        public int Id { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public DateTime DataPrazoDevolucao { get; set; }
        public ClienteForLocacao Cliente { get; set; }
        public FilmeForLocacao Filme { get; set; }
        public StatusLocacao Status { get; set; }
    }

    public class ClienteForLocacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class FilmeForLocacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}
