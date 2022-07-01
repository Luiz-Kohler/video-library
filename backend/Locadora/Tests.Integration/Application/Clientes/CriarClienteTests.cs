using Application.Services.Clientes.Criar;
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
    public class CriarClienteTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_criar_cliente()
        {
            var faker = new Faker();

            var request = Builder<CriarClienteRequest>
                .CreateNew() 
                .With(c => c.Cpf, CpfUtils.GerarCpf())
                .With(c => c.Nome, faker.Name.FullName())
                .With(c => c.DataNascimento, DateTime.UtcNow)
                .Build();

            await Handle<CriarClienteRequest, CriarClienteResponse>(request);

            var clientesDb = GetEntities<Cliente>();
            clientesDb.Should().HaveCount(1);

            var cliente = clientesDb.First();
            cliente.Nome.Should().Be(request.Nome);
            cliente.Cpf.Should().Be(request.Cpf);
        }
    }
}
