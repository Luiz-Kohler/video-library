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
            var criadoPor = _faker.Name.FirstName();

            var entidadeBase = new BaseEntityForTests(criadoPor);

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoEm.Should().BeNull();
            entidadeBase.UltimaAtualizacaoPor.Should().BeNull();
            entidadeBase.CriadoPor.Should().Be(criadoPor);
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_atualizar_entidade_base()
        {
            var criadoPor = _faker.Name.FirstName();
            var atualizadoPor = _faker.Random.String();

            var entidadeBase = new BaseEntityForTests(criadoPor);
            entidadeBase.UltimaAtualizacaoEm.Should().BeNull();
            entidadeBase.UltimaAtualizacaoPor.Should().BeNull();

            entidadeBase.AtualizarEntidadeBase(atualizadoPor);

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoPor.Should().Be(atualizadoPor);
            entidadeBase.CriadoPor.Should().Be(criadoPor);
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_desativar_entidade_base()
        {
            var criadoPor = _faker.Name.FirstName();
            var atualizadoPor = _faker.Random.String();

            var entidadeBase = new BaseEntityForTests(criadoPor);
            entidadeBase.EhAtivo.Should().BeTrue();

            entidadeBase.Desativar(atualizadoPor);

            entidadeBase.Id.Should().Be(0);
            entidadeBase.UltimaAtualizacaoPor.Should().Be(atualizadoPor);
            entidadeBase.CriadoPor.Should().Be(criadoPor);
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Deve_ativar_entidade_base()
        {
            var criadoPor = _faker.Name.FirstName();
            var atualizadoPor = _faker.Random.String();

            var entidadeBase = new BaseEntityForTests(criadoPor);
            entidadeBase.Desativar(atualizadoPor);
            entidadeBase.EhAtivo.Should().BeFalse();

            entidadeBase.Ativar(atualizadoPor);

            entidadeBase.Id.Should().Be(0);
            entidadeBase.EhAtivo.Should().BeTrue();
            entidadeBase.UltimaAtualizacaoPor.Should().Be(atualizadoPor);
            entidadeBase.CriadoPor.Should().Be(criadoPor);
            entidadeBase.UltimaAtualizacaoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
            entidadeBase.CriadoEm.Should().BeCloseTo(_dataAtual, TimeSpan.FromSeconds(10));
        }
    }
}
