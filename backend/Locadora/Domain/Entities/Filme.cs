using Domain.Common;
using Domain.Common.Enums;

namespace Domain.Entities
{
    public class Filme : BaseEntity
    {
        public string Titulo { get; private set; }
        public Classificacao Classificacao { get; private set; }
        public bool EhLancamento { get; private set; }
        public virtual ICollection<Locacao> Locacoes { get; }

        public Filme(int id, string titulo, Classificacao classificacao, bool ehLancamento)
            : base()
        {
            Id = id;
            Titulo = titulo;
            Classificacao = classificacao;
            EhLancamento = ehLancamento;
        }

        public void Atualizar(string titulo, Classificacao classificacao, bool ehLancamento)
        {
            Titulo = titulo;
            Classificacao = classificacao;
            EhLancamento = ehLancamento;
            AtualizarEntidadeBase();
        }
    }
}
