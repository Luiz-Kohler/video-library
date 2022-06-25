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
            var criadoPor = _faker.Name.FirstName();
            var nome = _faker.Name.FirstName();
            var cpf = CpfUtils.GerarCpf();
            var dataNascimento = DateTime.MinValue;

            var cliente = new Cliente(criadoPor, cpf, nome, dataNascimento);

            cliente.CriadoPor.Should().Be(criadoPor);
            cliente.Nome.Should().Be(nome);
            cliente.CPF.Should().Be(cpf);
            cliente.DataNascimento.Should().Be(dataNascimento);
        }
    }
}
