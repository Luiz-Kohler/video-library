using Domain.Common;

namespace Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public virtual ICollection<Locacao> Locacoes { get; }

        public Cliente(string criadoPor, string cpf, string nome, DateTime dataNascimento)
            : base(criadoPor)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
