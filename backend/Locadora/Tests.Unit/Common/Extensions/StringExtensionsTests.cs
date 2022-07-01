using Domain.Common.Extensios;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Unit.Common.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Deve_criar_cpf()
        {
            var cpf = CpfUtils.GerarCpf();

            cpf.IsCpf().Should().BeTrue();
        }

        [Fact]
        public void Deve_validar_cpf()
        {
            var cpfValido = CpfUtils.GerarCpf();
            var cpfInvalido = "1111.111.111-11";

            cpfValido.IsCpf().Should().BeTrue();
            cpfInvalido.IsCpf().Should().BeFalse();
        }

        [Fact]
        public void Deve_formatar_cpf()
        {
            var cpfInvalido = "111.111.111-11";

            cpfInvalido.FormatCpf().Should().Be("11111111111");
        }
    }
}
