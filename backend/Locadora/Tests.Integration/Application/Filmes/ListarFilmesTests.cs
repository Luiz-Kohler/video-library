using Application.Services.Filmes.Listar;
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
    public class ListarFilmesTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_listar_filmes()
        {
            var faker = new Faker();

            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(filmeInserir);

            var request = Builder<ListarFilmesRequest>
                .CreateNew()
                .Build();

            var response = await Handle<ListarFilmesRequest, ListarFilmesResponse>(request);

            response.Filmes.Should().HaveCount(1);

            response.Filmes.First().Id.Should().Be(filmeInserir.Id);
            response.Filmes.First().Titulo.Should().Be(filmeInserir.Titulo);
            response.Filmes.First().Classificacao.Should().Be(filmeInserir.Classificacao);
            response.Filmes.First().EhLancamento.Should().Be(filmeInserir.EhLancamento);
        }
    }
}
