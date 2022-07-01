using Application.Services.Locacoes.Buscar;
using Bogus;
using Domain.Common.Enums;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Locacoes
{
    public class BuscarLocacaoTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_buscar_locacao()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            var locacaoInserir = new Locacao(clienteInserir, filmeInserir);
            InsertOne(locacaoInserir);

            var request = Builder<BuscarLocacaoRequest>
                .CreateNew()
                .With(l => l.Id, locacaoInserir.Id)
                .Build();

            var response = await Handle<BuscarLocacaoRequest, BuscarLocacaoResponse>(request);

            response.Id.Should().Be(locacaoInserir.Id);
            response.Status.Should().Be(locacaoInserir.Status);
            response.DataDevolucao.Should().Be(locacaoInserir.DataDevolucao);

            response.Filme.Id.Should().Be(filmeInserir.Id);
            response.Filme.Titulo.Should().Be(filmeInserir.Titulo);

            response.Cliente.Id.Should().Be(clienteInserir.Id);
            response.Cliente.Nome.Should().Be(clienteInserir.Nome);
        }
    }
}
