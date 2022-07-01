using Application.Services.Clientes.Listar;
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
    public class ListarClientesTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_listar_clientes()
        {
            var faker = new Faker();

            var clienteInserir = new Cliente(CpfUtils.GerarCpf(), faker.Name.FullName(), DateTime.UtcNow);
            InsertOne(clienteInserir);

            var request = Builder<ListarClientesRequest>
                .CreateNew()
                .Build();

            var response = await Handle<ListarClientesRequest, ListarClientesResponse>(request);

            response.Clientes.Should().HaveCount(1);

            response.Clientes.First().Id.Should().Be(clienteInserir.Id);
            response.Clientes.First().Nome.Should().Be(clienteInserir.Nome);
            response.Clientes.First().CPF.Should().Be(clienteInserir.Cpf);
        }
    }
}