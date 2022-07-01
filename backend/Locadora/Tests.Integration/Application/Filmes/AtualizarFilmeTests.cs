using Application.Services.Filmes.Atualizar;
using Bogus;
using Domain.Common.Enums;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration.Application.Filmes
{
    public class AtualizarFilmeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_atualizar_filme()
        {
            var faker = new Faker();

            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(filmeInserir);

            var request = Builder<AtualizarFilmeRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .With(c => c.Titulo, faker.Name.FullName())
                .With(c => c.EhLancamento, faker.Random.Bool())
                .With(f => f.Classificacao, Classificacao.Dezoito)
                .Build();

            await Handle<AtualizarFilmeRequest, AtualizarFilmeResponse>(request);

            var filmesDb = GetEntities<Filme>();
            filmesDb.Should().HaveCount(1);

            var filme = filmesDb.First();
            filme.Id.Should().Be(request.Id);
            filme.Titulo.Should().Be(request.Titulo);
            filme.EhLancamento.Should().Be(request.EhLancamento);
            filme.Classificacao.Should().Be(request.Classificacao);
        }
    }
}
