using Bogus;
using Domain.Entities;
using FluentAssertions;
using System;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Unit.Entities
{
    public class ClienteTests
    {
        private readonly Faker _faker;

        public ClienteTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Deve_criar_cliente()
        {
            var nome = _faker.Name.FirstName();
            var cpf = CpfUtils.GerarCpf();
            var dataNascimento = DateTime.MinValue;

            var cliente = new Cliente(cpf, nome, dataNascimento);

            cliente.Nome.Should().Be(nome);
            cliente.Cpf.Should().Be(cpf);
            cliente.DataNascimento.Should().Be(dataNascimento);
        }
    }
}
