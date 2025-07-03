using Microsoft.EntityFrameworkCore;
using TesteTecnicoEffecti.Src.Models;

namespace TesteTecnicoEffecti.Src.Data.Mappings; 

public class ItemLicitacaoMapping : IEntityTypeConfiguration<ItemLicitacao>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ItemLicitacao> builder)
    {
        builder.ToTable("ItensLicitacao");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.TratamentoDiferenciado)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.Aplicabilidade7174)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.AplicabilidadeMargemPreferencia)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.UnidadeFornecimento)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.LicitacaoId)
            .IsRequired();

        builder.HasOne(x => x.Licitacao)
            .WithMany(x => x.Itens)
            .HasForeignKey(x => x.LicitacaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
