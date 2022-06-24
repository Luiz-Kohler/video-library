using Domain.Common;

namespace Domain.Entities
{
    public class Locacao : BaseEntity
    {
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public bool FoiDevolvido { get; private set; }
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
        }

        public Locacao(string criadoPor, Cliente cliente, Filme filme)
            : base(criadoPor)
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
            DataDevolucao = CalcularDataDevolucao();
        }

        public void Devolvido(string atualizadoPor)
        {
            FoiDevolvido = true;
            AtualizarEntidadeBase(atualizadoPor);
        }


        private DateTime CalcularDataDevolucao()
        {
            var diasParaDevolver = Filme.EhLancamento ? 2 : 3;
            return DataLocacao.AddDays(diasParaDevolver);
        }
    }
}
