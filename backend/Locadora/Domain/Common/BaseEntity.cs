namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; }
        public DateTime CriadoEm { get; private init; }
        public string CriadoPor { get; private init; }
        public DateTime? UltimaAtualizacaoEm { get; protected set; }
        public string UltimaAtualizacaoPor { get; protected set; }
        public bool EhAtivo { get; private set; }

        public BaseEntity(string criadoPor)
        {
            CriadoEm = DateTime.UtcNow;
            CriadoPor = criadoPor;
            EhAtivo = true;
        }

        public void Desativar(string atualizadoPor)
        {
            EhAtivo = false;
            AtualizarEntidadeBase(atualizadoPor);
        }
        public void Ativar(string atualizadoPor)
        {
            EhAtivo = true;
            AtualizarEntidadeBase(atualizadoPor);
        }

        public void AtualizarEntidadeBase(string atualizadoPor)
        {
            UltimaAtualizacaoEm = DateTime.UtcNow;
            UltimaAtualizacaoPor = atualizadoPor;
        }
    }
}
