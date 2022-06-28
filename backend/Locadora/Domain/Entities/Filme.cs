using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Validators;

namespace Domain.Entities
{
    public class Filme : BaseEntity<Filme>
    {
        public string Titulo { get; private set; }
        public Classificacao Classificacao { get; private set; }
        public bool EhLancamento { get; private set; }
        public virtual ICollection<Locacao> Locacoes { get; }

        public Filme(string titulo, Classificacao classificacao, bool ehLancamento)
            : base()
        {
            Titulo = titulo;
            Classificacao = classificacao;
            EhLancamento = ehLancamento;

            Validar<FilmesValidator>();
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
