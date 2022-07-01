using Application.Services.Clientes.Buscar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Clientes
{
    public class BuscarClienteTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_buscar_cliente()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(clienteInserir);

            var request = Builder<BuscarClienteRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            var response = await Handle<BuscarClienteRequest, BuscarClienteResponse>(request);

            response.Id.Should().Be(clienteInserir.Id);
            response.Nome.Should().Be(clienteInserir.Nome);
            response.CPF.Should().Be(clienteInserir.Cpf);
        }
    }
}
