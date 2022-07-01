using Domain.Entities;
using Infrastructure.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Mapping
{
    public class ClienteMapping : BaseMapping<Cliente>
    {
        public override string TableName => "clientes";

        protected override void MapearEntidade(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Nome).HasColumnName("nome").HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(c => c.Cpf).HasColumnName("cpf").HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(c => c.DataNascimento).HasColumnName("data_nascimento").HasColumnType("DATETIME").IsRequired();

            builder.HasMany(c => c.Locacoes)
                .WithOne(l => l.Cliente)
                .HasForeignKey(l => l.ClienteId)
                .HasPrincipalKey(l => l.Id);
        }

        protected override void MapearIndices(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasIndex(c => new { c.Id, c.EhAtivo }, "ix_id_ativo");
            builder.HasIndex(c => new { c.Nome, c.EhAtivo }, "ix_nome_ativo");
            builder.HasIndex(c => new { c.Cpf, c.EhAtivo }, "ix_cpf_ativo");
        }
    }
}
