using Application.Services.Clientes.Excluir;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Clientes
{
    public class ExcluirClienteTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_excluir_cliente()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(clienteInserir);

            var request = Builder<ExcluirClienteRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            await Handle<ExcluirClienteRequest, ExcluirClienteResponse>(request);

            var clientesAtivosDb = GetEntities<Cliente>().Where(cliente => cliente.EhAtivo);
            clientesAtivosDb.Should().HaveCount(0);
        }
    }
}
