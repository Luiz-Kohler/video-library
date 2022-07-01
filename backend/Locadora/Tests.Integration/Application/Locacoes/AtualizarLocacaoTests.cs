using Application.Services.Locacoes.Atualizar;
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
    public class AtualizarLocacaoTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_atualizar_locacao()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            var locacaoInserir = new Locacao(clienteInserir, filmeInserir);
            InsertOne(locacaoInserir);

            var novoCliente = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(novoCliente);

            var novoFilme = new Filme(2, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            InsertOne(novoFilme);

            var request = Builder<AtualizarLocacaoRequest>
                .CreateNew()
                .With(l => l.LocacaoId, locacaoInserir.Id)
                .With(l => l.ClienteId, novoCliente.Id)
                .With(l => l.FilmeId, novoFilme.Id)
                .Build();

            await Handle<AtualizarLocacaoRequest, AtualizarLocacaoResponse>(request);

            var locacoesDb = GetEntities<Locacao>();
            locacoesDb.Should().HaveCount(1);

            var locacao = locacoesDb.First();
            locacao.FilmeId.Should().Be(request.FilmeId);
            locacao.ClienteId.Should().Be(request.ClienteId);
        }
    }
}
