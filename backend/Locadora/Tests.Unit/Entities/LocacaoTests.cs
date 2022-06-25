using Bogus;
using Domain.Entities;
using FluentAssertions;
using System;
using Tests.Common.Builders;
using Xunit;

namespace Tests.Unit.Entities
{
    public class LocacaoTests
    {
        private readonly Faker _faker;

        public LocacaoTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Deve_criar_locacao_com_data_locacao()
        {
            var cliente = new ClienteBuilder().Construir();
            var filme = new FilmeBuilder().Construir();

            var criadoPor = _faker.Name.FirstName();
            var dataLocacao = DateTime.MinValue.ToUniversalTime();

            var locacao = new Locacao(criadoPor, dataLocacao, cliente, filme);

            locacao.CriadoPor.Should().Be(criadoPor);
            locacao.DataLocacao.Should().Be(dataLocacao);
        }

        [Fact]
        public void Deve_criar_locacao_sem_data_locacao()
        {
            var cliente = new ClienteBuilder().Construir();
            var filme = new FilmeBuilder().Construir();

            var criadoPor = _faker.Name.FirstName();
            var dataAtual = DateTime.UtcNow;

            var locacao = new Locacao(criadoPor, cliente.Id, filme.Id);

            locacao.CriadoPor.Should().Be(criadoPor);
            locacao.DataLocacao.Should().BeCloseTo(dataAtual, TimeSpan.FromSeconds(10));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Deve_criar_locacao_com_filmes_comum_e_lancamento(bool filmeEhLancamento)
        {
            var cliente = new ClienteBuilder().Construir();
            var filme = new FilmeBuilder().ComEhLancamento(filmeEhLancamento).Construir();

            var criadoPor = _faker.Name.FirstName();
            var dataAtual = DateTime.UtcNow;

            var diasParaDevolver = filmeEhLancamento ? 2 : 3;

            var locacao = new Locacao(criadoPor, cliente.Id, filme.Id);

            locacao.CriadoPor.Should().Be(criadoPor);
            locacao.DataLocacao.Should().BeCloseTo(dataAtual, TimeSpan.FromSeconds(10));
            locacao.DataDevolucao.Date.Should().Be(dataAtual.AddDays(diasParaDevolver).Date);
        }
    }
}
