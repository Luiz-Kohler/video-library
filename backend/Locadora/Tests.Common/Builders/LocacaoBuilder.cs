using Domain.Entities;

namespace Tests.Common.Builders
{
    public class LocacaoBuilder : BaseBuilder<LocacaoBuilder>
    {
        private DateTime? _dataLocacao;
        private Cliente _cliente;
        private Filme _filme;

        public Locacao Construir()
        {
            return new Locacao(
                _criadoPor ?? _faker.Name.FirstName(),
                _dataLocacao ?? DateTime.UtcNow,
                _cliente ?? new ClienteBuilder().Construir(),
                _filme ?? new FilmeBuilder().Construir());
        }

        public LocacaoBuilder ComDataLocacao(DateTime dataLocacao)
        {
            _dataLocacao = dataLocacao;
            return this;
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
