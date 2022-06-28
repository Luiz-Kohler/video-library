using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Validators;

namespace Domain.Entities
{
    public class Locacao : BaseEntity<Locacao>
    {
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public StatusLocacao Status { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public int FilmeId { get; private set; }
        public virtual Filme Filme { get; private set; }

        public Locacao(Cliente cliente, Filme filme)
            : base()
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
            DataDevolucao = CalcularDataDevolucao();
            Status = StatusLocacao.Andamento;
        }

        protected Locacao(int clienteId, int filmeId, DateTime dataLocacao, DateTime dataDevolucao)
            : base()
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = clienteId;
            FilmeId = filmeId;
            DataLocacao = dataLocacao;
            DataDevolucao = dataDevolucao;
            Status = StatusLocacao.Andamento;
        }

        public void AtualizarLocacao(Cliente cliente, Filme filme)
        {
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
        }

        private DateTime CalcularDataDevolucao()
        {
            var diasParaDevolver = Filme.EhLancamento ? 2 : 3;
            return DataLocacao.AddDays(diasParaDevolver);
        }
    }
}
