using Domain.Entities;
using Infrastructure.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Mapping
{
    internal class FilmeMapping : BaseMapping<Filme>
    {
        public override string TableName => "filmes";

        protected override void MapearEntidade(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsUnicode();

            builder.Property(f => f.Titulo).HasColumnName("titulo").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(f => f.EhLancamento).HasColumnName("lancamento").IsRequired();
            builder.Property(f => f.Classificacao).HasColumnName("classificacao").IsRequired();

            builder.HasMany(f => f.Locacoes)
                .WithOne(l => l.Filme)
                .HasForeignKey(l => l.FilmeId)
                .HasPrincipalKey(l => l.Id);
        }

        protected override void MapearIndices(EntityTypeBuilder<Filme> builder)
        {
            builder.HasIndex(f => new { f.Id, f.EhAtivo });
            builder.HasIndex(f => new { f.EhLancamento, f.EhAtivo });
            builder.HasIndex(f => new { f.Titulo, f.EhAtivo });
        }
    }
}
