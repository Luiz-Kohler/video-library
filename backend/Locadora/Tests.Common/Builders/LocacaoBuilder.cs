using Domain.Entities;

namespace Tests.Common.Builders
{
    public class LocacaoBuilder : BaseBuilder<LocacaoBuilder>
    {
        private Cliente _cliente;
        private Filme _filme;

        public Locacao Construir()
        {
            return new Locacao(
                _cliente ?? new ClienteBuilder().Construir(),
                _filme ?? new FilmeBuilder().Construir());
        }

        public LocacaoBuilder ComCliente(Cliente cliente)
        {
            _cliente = cliente;
            return this;
        }

        public LocacaoBuilder ComFilme(Filme filme)
        {
            _filme = filme;
            return this;
        }
    }
}
