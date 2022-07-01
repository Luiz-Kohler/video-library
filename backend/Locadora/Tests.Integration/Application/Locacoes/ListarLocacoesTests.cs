using Application.Services.Locacoes.Listar;
using Bogus;
using Domain.Common.Enums;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Locacoes
{
    public class ListarLocacoesTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_listar_locacoes()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            var locacaoInserir = new Locacao(clienteInserir, filmeInserir);
            InsertOne(locacaoInserir);

            var request = Builder<ListarLocacoesRequest>
                .CreateNew()
                .Build();

            var response = await Handle<ListarLocacoesRequest, ListarLocacoesResponse>(request);

            response.Locacoes.Should().HaveCount(1);

            response.Locacoes.First().Id.Should().Be(locacaoInserir.Id);
            response.Locacoes.First().Status.Should().Be(locacaoInserir.Status);
            response.Locacoes.First().DataDevolucao.Should().Be(locacaoInserir.DataDevolucao);

            response.Locacoes.First().Filme.Id.Should().Be(filmeInserir.Id);
            response.Locacoes.First().Filme.Titulo.Should().Be(filmeInserir.Titulo);

            response.Locacoes.First().Cliente.Id.Should().Be(clienteInserir.Id);
            response.Locacoes.First().Cliente.Nome.Should().Be(clienteInserir.Nome);
        }
    }
}
