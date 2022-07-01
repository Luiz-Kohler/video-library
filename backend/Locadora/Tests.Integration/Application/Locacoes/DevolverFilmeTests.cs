using Application.Services.Locacoes.Devolver;
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
    public class DevolverFilmeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_devolver_filme_locacao()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            var locacaoInserir = new Locacao(clienteInserir, filmeInserir);
            InsertOne(locacaoInserir);

            var request = Builder<DevolverFilmeRequest>
                .CreateNew()
                .With(l => l.Id, locacaoInserir.Id)
                .Build();

            await Handle<DevolverFilmeRequest, DevolverFilmeResponse>(request);

            var locacoes = GetEntities<Locacao>();
            locacoes.Should().HaveCount(1);
            locacoes.First().Status.Should().Be(StatusLocacao.Devolvido);
        }
    }
}
