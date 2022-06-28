using Domain.Entities;
using Infrastructure.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Mapping
{
    public class LocacaoMapping : BaseMapping<Locacao>
    {
        public override string TableName => "locacoes";

        protected override void MapearEntidade(EntityTypeBuilder<Locacao> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.DataLocacao).HasColumnName("data_locacao").HasColumnType("DATETIME").IsRequired();
            builder.Property(l => l.DataDevolucao).HasColumnName("data_prazo_devolucao").HasColumnType("DATETIME").IsRequired();
            builder.Property(l => l.DataDevolucao).HasColumnName("data_devolucao").HasColumnType("DATETIME").IsRequired(false);

            builder.Ignore(l => l.Filme);
            builder.Ignore(l => l.Status);
            builder.Ignore(l => l.Cliente);

            builder.HasOne(l => l.Cliente)
                .WithMany(c => c.Locacoes)
                .HasForeignKey(l => l.ClienteId)
                .HasPrincipalKey(c => c.Id);

            builder.HasOne(l => l.Filme)
                .WithMany(f => f.Locacoes)
                .HasForeignKey(l => l.FilmeId)
                .HasPrincipalKey(l => l.Id);
        }

        protected override void MapearIndices(EntityTypeBuilder<Locacao> builder)
        {
            builder.HasIndex(f => new { f.Id, f.EhAtivo }, "ix_id_ativo");
        }
    }
}
