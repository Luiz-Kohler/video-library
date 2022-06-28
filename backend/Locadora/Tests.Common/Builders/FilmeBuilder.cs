using Domain.Common.Enums;
using Domain.Entities;

namespace Tests.Common.Builders
{
    public class FilmeBuilder : BaseBuilder<FilmeBuilder>
    {
        private string _titulo;
        private Classificacao? _classificacao;
        private bool? _ehLancamento;

        public Filme Construir()
        {
            return new Filme(
                _titulo ?? _faker.Random.String(),
                _classificacao ?? Classificacao.Dez,
                _ehLancamento ?? _faker.Random.Bool());
        }

        public FilmeBuilder ComTitulo(string titulo)
        {
            _titulo = titulo;
            return this;
        }

        public FilmeBuilder ComClassificacaoIndicativa(Classificacao classificacao)
        {
            _classificacao = classificacao;
            return this;
        }

        public FilmeBuilder ComEhLancamento(bool ehLancamento)
        {
            _ehLancamento = ehLancamento;
            return this;
        }
    }
}
