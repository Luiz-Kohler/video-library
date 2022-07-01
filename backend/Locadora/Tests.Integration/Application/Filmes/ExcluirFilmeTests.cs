using Application.Services.Filmes.Excluir;
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
    public class ExcluirFilmeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_excluir_filme()
        {
            var faker = new Faker();

            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(filmeInserir);

            var request = Builder<ExcluirFilmeRequest>
                .CreateNew()
                .With(f => f.Id, 1)
                .Build();

            await Handle<ExcluirFilmeRequest, ExcluirFilmeResponse>(request);

            var filmeAtivosDb = GetEntities<Filme>().Where(filme => filme.EhAtivo);
            filmeAtivosDb.Should().HaveCount(0);
        }
    }
}
