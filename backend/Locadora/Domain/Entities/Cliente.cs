﻿using Domain.Common;
using Domain.Common.Extensios;

namespace Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public virtual ICollection<Locacao> Locacoes { get; }

        public Cliente(string cpf, string nome, DateTime dataNascimento)
            : base()
        {
            Nome = nome;
            Cpf = cpf.FormatCpf();
            DataNascimento = dataNascimento.ToUniversalTime();
        }

        public void Atualizar(string nome, DateTime dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            AtualizarEntidadeBase();
        }
    }
}
