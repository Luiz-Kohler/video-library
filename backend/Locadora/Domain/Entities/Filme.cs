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

        public Filme(string criadoPor, string titulo, Classificacao classificacao, bool ehLancamento)
            : base(criadoPor)
        {
            Titulo = titulo;
            Classificacao = classificacao;
            EhLancamento = ehLancamento;
        }
    }
}
