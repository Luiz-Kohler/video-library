using Application.Services.Filmes.Buscar;
using Bogus;
using Domain.Common.Enums;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration.Application.Filmes
{
    public class BuscarFilmeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_buscar_filme()
        {
            var faker = new Faker();

            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(filmeInserir);

            var request = Builder<BuscarFilmeRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            var response = await Handle<BuscarFilmeRequest, BuscarFilmeResponse>(request);

            response.Id.Should().Be(filmeInserir.Id);
            response.Titulo.Should().Be(filmeInserir.Titulo);
            response.Classificacao.Should().Be(filmeInserir.Classificacao);
            response.EhLancamento.Should().Be(filmeInserir.EhLancamento);
        }
    }
}
