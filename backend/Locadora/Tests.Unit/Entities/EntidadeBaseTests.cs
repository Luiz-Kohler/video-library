using Bogus;
using FluentAssertions;
using System;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Unit.Entities
{
    public class EntidadeBaseTests
    {
        private readonly Faker _faker;
        private readonly DateTime _dataAtual;

        public EntidadeBaseTests()
        {
            _faker = new Faker();
            _dataAtual = DateTime.UtcNow;
        }

        [Fact]
        public void Deve_criar_entidade_base()
        {
            var entidadeBase = new BaseEntityForTests();

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoEm.Should().BeNull();
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_atualizar_entidade_base()
        {
            var entidadeBase = new BaseEntityForTests();
            entidadeBase.UltimaAtualizacaoEm.Should().BeNull();

            entidadeBase.AtualizarEntidadeBase();

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_desativar_entidade_base()
        {
            var entidadeBase = new BaseEntityForTests();
            entidadeBase.EhAtivo.Should().BeTrue();

            entidadeBase.Desativar();

            entidadeBase.Id.Should().Be(0);
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_ativar_entidade_base()
        {
            var entidadeBase = new BaseEntityForTests();
            entidadeBase.EhAtivo.Should().BeFalse();

            entidadeBase.Ativar();

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }
    }
}
