using Domain.Common;
using Domain.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Locacao : BaseEntity
    {
        public DateTime DataLocacao { get; private set; }
        public DateTime DataPrazoDevolucao { get; private set; }
        public DateTime? DataDevolucao { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public int FilmeId { get; private set; }
        public virtual Filme Filme { get; private set; }

        [NotMapped]
        public StatusLocacao Status => BuscarStatus();

        public Locacao(Cliente cliente, Filme filme)
            : base()
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
            DataPrazoDevolucao = CalcularDataDevolucao();
        }

        protected Locacao(int clienteId, int filmeId, DateTime dataLocacao, DateTime dataPrazoDevolucao, DateTime? dataDevolucao)
            : base()
        {
            DataLocacao = DateTime.UtcNow;
            ClienteId = clienteId;
            FilmeId = filmeId;
            DataLocacao = dataLocacao;
            DataPrazoDevolucao = dataPrazoDevolucao;
            DataDevolucao = dataDevolucao;
        }

        public void AtualizarLocacao(Cliente cliente, Filme filme)
        {
            ClienteId = cliente.Id;
            Cliente = cliente;
            FilmeId = filme.Id;
            Filme = filme;
        }

        public void DevolverFilme()
        {
            DataDevolucao = DateTime.UtcNow;
        }

        public StatusLocacao BuscarStatus()
        {
            if (DataDevolucao.HasValue && DataDevolucao > DataPrazoDevolucao)
                return StatusLocacao.DevolvidoComAtraso;
            else if (DataDevolucao.HasValue && DataDevolucao < DataPrazoDevolucao)
                return StatusLocacao.Devolvido;
            else if (!DataDevolucao.HasValue && DateTime.UtcNow > DataPrazoDevolucao)
                return StatusLocacao.Atrasado;
            else 
                return StatusLocacao.Andamento;
        }

        private DateTime CalcularDataDevolucao()
        {
            var diasParaDevolver = Filme.EhLancamento ? 2 : 3;
            return DataLocacao.AddDays(diasParaDevolver);
        }
    }
}
