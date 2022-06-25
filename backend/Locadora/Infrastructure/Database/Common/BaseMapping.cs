using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Common
{
    public abstract class BaseMapping<TEntity> : IBaseMapping
        where TEntity : BaseEntity
    {
        public abstract string TableName { get; }

        public void MapearEntidade(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<TEntity>();
            MapearBase(entityBuilder);
            MapearEntidade(entityBuilder);
            MapearIndices(entityBuilder);
        }

        private void MapearBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CriadoEm).HasColumnName("criado_em").HasColumnType("DATETIME").IsRequired();
            builder.Property(e => e.UltimaAtualizacaoEm).HasColumnName("ultima_atualizacao_em").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(e => e.CriadoPor).HasColumnName("criado_por").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(e => e.UltimaAtualizacaoPor).HasColumnName("ultima_atualizacao_por").HasColumnType("VARCHAR(100)").IsRequired(false);
            builder.Property(e => e.EhAtivo).HasColumnName("ativo").IsRequired();
        }

        protected abstract void MapearEntidade(EntityTypeBuilder<TEntity> builder);
        protected virtual void MapearIndices(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasIndex(e => e.Id);
        }
    }
}
