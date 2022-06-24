using Domain.Entities;
using Tests.Common.Helpers;

namespace Tests.Common.Builders
{
    public class ClienteBuilder : BaseBuilder<ClienteBuilder>
    {
        private string _nome;
        private string _cpf;
        private DateTime? _dataNascimento;

        public Cliente Construir()
        {
            return new Cliente(
                _criadoPor ?? _faker.Name.FirstName(), 
                _nome ?? _faker.Name.FirstName(), 
                _cpf ?? CpfUtils.GerarCpf(), 
                _dataNascimento ?? DateTime.UtcNow);
        }

        public ClienteBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ClienteBuilder ComCPF(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public ClienteBuilder ComDataNascimento(DateTime dataNascimento)
        {
            _dataNascimento = dataNascimento;
            return this;
        }
    }
}
