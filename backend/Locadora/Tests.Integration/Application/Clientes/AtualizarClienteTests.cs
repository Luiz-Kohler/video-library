using Application.Services.Clientes.Atualizar;
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
    public class AtualizarClienteTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_atualizar_cliente()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(clienteInserir);

            var request = Builder<AtualizarClienteRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .With(c => c.Nome, faker.Name.FullName())
                .With(c => c.DataNascimento, DateTime.UtcNow)
                .Build();

            await Handle<AtualizarClienteRequest, AtualizarClienteResponse>(request);

            var clientesDb = GetEntities<Cliente>();
            clientesDb.Should().HaveCount(1);

            var cliente = clientesDb.First();
            cliente.Cpf.Should().Be(clienteInserir.Cpf);
            cliente.Nome.Should().Be(request.Nome);
        }  
    }
}
