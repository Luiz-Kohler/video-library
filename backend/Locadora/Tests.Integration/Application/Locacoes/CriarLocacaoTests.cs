using Application.Services.Locacoes.Criar;
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
    public class CriarLocacaoTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_criar_locacao()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(clienteInserir);

            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(filmeInserir);

            var request = Builder<CriarLocacaoRequest>
                .CreateNew()
                .With(l => l.ClienteId, clienteInserir.Id)
                .With(l => l.FilmeId, filmeInserir.Id)
                .Build();

            await Handle<CriarLocacaoRequest, CriarLocacaoResponse>(request);

            var locacoesDb = GetEntities<Locacao>();
            locacoesDb.Should().HaveCount(1);

            var locacao = locacoesDb.First();
            locacao.FilmeId.Should().Be(request.FilmeId);
            locacao.ClienteId.Should().Be(request.ClienteId);
        }
    }
}
