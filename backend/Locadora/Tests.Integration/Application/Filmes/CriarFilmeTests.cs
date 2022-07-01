using Application.Services.Filmes.Criar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration.Application.Filmes
{
    public class CriarFilmeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_criar_filme()
        {
            var faker = new Faker();

            var request = Builder<CriarFilmeRequest>
                .CreateNew()
                .With(f => f.Titulo, faker.Random.Words())
                .With(f => f.EhLancamento, faker.Random.Bool())
                .With(f => f.Id, faker.Random.Number(1, int.MaxValue))
                .Build();

            await Handle<CriarFilmeRequest, CriarFilmeResponse>(request);

            var filmesDb = GetEntities<Filme>();
            filmesDb.Should().HaveCount(1);

            var filme = filmesDb.First();
            filme.Id.Should().Be(request.Id);
            filme.EhLancamento.Should().Be(request.EhLancamento);
            filme.Titulo.Should().Be(request.Titulo);
        }
    }
}
