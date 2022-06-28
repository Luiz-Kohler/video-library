using Bogus;
using Domain.Common.Enums;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Tests.Unit.Entities
{
    public class FilmesTests
    {
        private readonly Faker _faker;

        public FilmesTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Deve_criar_filme()
        {
            var id = _faker.Random.Number();
            var titulo = _faker.Random.String();
            var classificacao = Classificacao.Dez;
            var ehLancamento = _faker.Random.Bool();

            var filme = new Filme(id, titulo, classificacao, ehLancamento);

            filme.Id.Should().Be(id);
            filme.Titulo.Should().Be(titulo);
            filme.Classificacao.Should().Be(classificacao);
            filme.EhLancamento.Should().Be(ehLancamento);
        }
    }
}
