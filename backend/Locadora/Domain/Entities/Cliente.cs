using Domain.Common;

namespace Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public virtual ICollection<Locacao> Locacoes { get; private set; }

        public Cliente(string criadoPor, string nome, string cpf, DateTime dataNascimento)
            : base(criadoPor)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
