using FluentValidation;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set;  }
        public DateTime CriadoEm { get; private init; }
        public DateTime? UltimaAtualizacaoEm { get; protected set; }
        public bool EhAtivo { get; private set; }

        public BaseEntity()
        {
            CriadoEm = DateTime.UtcNow;
            EhAtivo = true;
        }

        public void Desativar()
        {
            EhAtivo = false;
            AtualizarEntidadeBase();
        }

        public void Ativar()
        {
            EhAtivo = true;
            AtualizarEntidadeBase();
        }

        public void AtualizarEntidadeBase()
        {
            UltimaAtualizacaoEm = DateTime.UtcNow;
        }
    }
}
