using Application.Services.Locacoes.Excluir;
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
    public class ExcluirLocacaoTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_excluir_locacao()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            var filmeInserir = new Filme(1, faker.Random.Words(), Classificacao.Livre, faker.Random.Bool());
            var locacaoInserir = new Locacao(clienteInserir, filmeInserir);
            InsertOne(locacaoInserir);

            var request = Builder<ExcluirLocacaoRequest>
                .CreateNew()
                .With(l => l.Id, locacaoInserir.Id)
                .Build();

            await Handle<ExcluirLocacaoRequest, ExcluirLocacaoResponse>(request);

            var locacoesAtivasDb = GetEntities<Locacao>().Where(locacao => locacao.EhAtivo);
            locacoesAtivasDb.Should().HaveCount(0);
        }
    }
}
