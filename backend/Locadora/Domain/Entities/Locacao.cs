using Domain.Common;
using Domain.Common.Enums;

namespace Domain.Entities
{
    public class Locacao : BaseEntity
    {
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public StatusLocacao Status { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public int FilmeId { get; private set; }
        public virtual Filme Filme { get; private set; }

        public Locacao(string criadoPor, DateTime dataLocacao, Cliente cliente, Filme filme)
            : base(criadoPor)
        {
            DataLocacao = dataLocacao.ToUniversalTime();
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
            DataDevolucao = CalcularDataDevolucao();
            Status = StatusLocacao.Andamento;
        }

        public Locacao(string criadoPor, int clienteId, int filmeId)
            : base(criadoPor)
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = clienteId;
            FilmeId = filmeId;
            DataDevolucao = CalcularDataDevolucao();
            Status = StatusLocacao.Andamento;
        }

        private DateTime CalcularDataDevolucao()
        {
            var diasParaDevolver = Filme.EhLancamento ? 2 : 3;
            return DataLocacao.AddDays(diasParaDevolver);
        }
    }
}
